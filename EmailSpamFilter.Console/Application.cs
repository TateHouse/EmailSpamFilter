namespace EmailSpamFilter.Console;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Utilities;
using EmailSpamFilter.Core.Filters;

public class Application
{
	private readonly IEmailLoaderFactory emailLoaderFactory;
	private readonly IEmailParserFactory emailParserFactory;
	private readonly ISpamFilterProvider spamFilterProvider;
	private readonly ISpamEmailFilterFactory spamEmailFilterFactory;
	private readonly IConsoleUserInterface consoleUserInterface;

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