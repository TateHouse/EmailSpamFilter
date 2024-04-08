namespace EmailSpamFilter.Console;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Utilities;
using EmailSpamFilter.Core.Filters;

/// <summary>
/// A class representing the console application.
/// </summary>
public class Application
{
	private readonly IEmailLoaderFactory emailLoaderFactory;
	private readonly IEmailParserFactory emailParserFactory;
	private readonly ISpamFilterProvider spamFilterProvider;
	private readonly ISpamEmailFilterFactory spamEmailFilterFactory;
	private readonly IConsoleUserInterface consoleUserInterface;

	/// <summary>
	/// Instantiates a new <see cref="Application"/>.
	/// </summary>
	/// <param name="emailLoaderFactory">An instance of <see cref="IEmailLoaderFactory"/> used for loading emails.</param>
	/// <param name="emailParserFactory">An instance of <see cref="IEmailParserFactory"/> used for parsing
	/// <see cref="LoadedEmail"/>.</param>
	/// <param name="spamFilterProvider">An instance of <see cref="ISpamFilterProvider"/> used for providing
	/// <see cref="ISpamFilter"/>.</param>
	/// <param name="spamEmailFilterFactory">An instance of <see cref="ISpamEmailFilterFactory"/> used for providing
	/// <see cref="ISpamEmailFilter"/> instances for applying  <see cref="ISpamFilter"/> on emails.</param>
	/// <param name="consoleUserInterface">An instance of <see cref="IConsoleUserInterface"/> used for providing the
	/// console user interface.</param>
	public Application(IEmailLoaderFactory emailLoaderFactory,
					   IEmailParserFactory emailParserFactory,
					   ISpamFilterProvider spamFilterProvider,
					   ISpamEmailFilterFactory spamEmailFilterFactory,
					   IConsoleUserInterface consoleUserInterface)
	{
		this.emailLoaderFactory = emailLoaderFactory;
		this.emailParserFactory = emailParserFactory;
		this.spamFilterProvider = spamFilterProvider;
		this.spamEmailFilterFactory = spamEmailFilterFactory;
		this.consoleUserInterface = consoleUserInterface;
	}

	/// <summary>
	/// Runs the application.
	/// </summary>
	public async Task Run()
	{
		var emailLoader = emailLoaderFactory.Create();
		var loadedEmails = await emailLoader.LoadAsync();
		var parsedEmails = ParseEmails(loadedEmails);

		consoleUserInterface.DisplayAvailableFilters();

		var filters = consoleUserInterface.SelectFilters();
		var filteredEmails = await FilterEmails(parsedEmails, filters);

		const byte indentationLevel = 1;
		consoleUserInterface.DisplayFilteredEmails(filteredEmails, indentationLevel);
	}

	/// <summary>
	/// Parses emails in parallel.
	/// </summary>
	/// <param name="loadedEmails">A collection of <see cref="LoadedEmail"/>.</param>
	/// <returns>A collection of <see cref="ParsedEmail"/>.</returns>
	private IEnumerable<ParsedEmail> ParseEmails(IEnumerable<LoadedEmail> loadedEmails)
	{
		var parsedEmails = new List<ParsedEmail>();

		Parallel.ForEach(loadedEmails,
						 loaded =>
						 {
							 var parser = emailParserFactory.Create(loaded);
							 var email = parser.Parse();
							 parsedEmails.Add(email);
						 });

		return parsedEmails;
	}

	/// <summary>
	/// Applies <see cref="ISpamFilter"/> to emails in parallel to determine which are spam and which are not.
	/// </summary>
	/// <param name="parsedEmails">A collection of <see cref="ParsedEmail"/>.</param>
	/// <param name="spamFilterIds">A collection of <see cref="ISpamFilter"/> ids for the <see cref="ISpamFilter"/>
	/// to apply.</param>
	/// <returns>A Task representing the asynchronous operation. The Task's result is a collection of
	/// <see cref="FilteredEmail"/>.</returns>
	private async Task<IEnumerable<FilteredEmail>> FilterEmails(IEnumerable<ParsedEmail> parsedEmails,
																IEnumerable<byte> spamFilterIds)
	{
		var filteredEmails = new List<FilteredEmail>();

		var tasks = parsedEmails.Select(async parsedEmail =>
		{
			var spamFilters = spamFilterIds.Select(spamFilterType => (SpamFilterType)spamFilterType)
										   .Select(spamFilterProvider.Create)
										   .ToList();
			var spamEmailFilter = spamEmailFilterFactory.Create(spamFilters, parsedEmail);
			var filteredEmail = await spamEmailFilter.FilterAsync();
			filteredEmails.Add(filteredEmail);
		});

		await Task.WhenAll(tasks);

		return filteredEmails;
	}
}