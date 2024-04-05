namespace EmailSpamFilter.Console;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Utilities;
using EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Utilities;
using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;

public class Application
{
	private readonly IConfiguration configuration;
	private readonly ISpamFilterProvider spamFilterProvider;
	private readonly ConsoleUserInterface consoleUserInterface;

	public Application(string appSettingsPath, string secretsPath)
	{
		if (string.IsNullOrWhiteSpace(appSettingsPath))
		{
			throw new ArgumentException("The app settings path must be provided", nameof(appSettingsPath));
		}

		var configurationBuilder = new ConfigurationBuilder();
		configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
		configurationBuilder.AddJsonFile(appSettingsPath, false, true);
		configurationBuilder.AddJsonFile(secretsPath, false, true);

		configuration = configurationBuilder.Build();

		var spamKeywordsPath = configuration["SpamKeywordsFile"];

		if (string.IsNullOrWhiteSpace(spamKeywordsPath))
		{
			throw new ArgumentException("The spam keywords path must be provided", nameof(spamKeywordsPath));
		}

		var keywordHasher = new KeywordHasherSHA256();
		var linkExtractor = new RegexLinkExtractor();
		var linkSafetyChecker = new GoogleSafeBrowsingLinkSafetyChecker(configuration);
		var spamKeywords = new HashSet<string>(File.ReadAllLines(spamKeywordsPath)).ToImmutableHashSet();
		var spamFilterFactory = new SpamFilterFactory(keywordHasher, linkExtractor, linkSafetyChecker, spamKeywords);

		spamFilterProvider = new SpamFilterProvider(spamFilterFactory);

		var filteredEmailStringBuilder = new FilteredEmailStringBuilder();
		consoleUserInterface = new ConsoleUserInterface(spamFilterProvider.AvailableSpamFilterTypes,
														filteredEmailStringBuilder);
	}

	public async Task Run()
	{
		var emailLoader = new TextEmailLoader(configuration["EmailsPath"]);
		var loadedEmails = (await emailLoader.LoadAsync()).ToList();
		var parsedEmails = ParseEmailsInParallel(loadedEmails);

		consoleUserInterface.DisplayAvailableFilters();

		var selectedFilters = consoleUserInterface.SelectFilters();
		var filteredEmails = await FilterEmailsInParallel(parsedEmails, selectedFilters);

		const byte indentationLevel = 1;
		consoleUserInterface.DisplayFilteredEmails(filteredEmails, indentationLevel);
	}

	private IEnumerable<ParsedEmail> ParseEmailsInParallel(IEnumerable<LoadedEmail> loadedEmails)
	{
		var parsedEmails = new List<ParsedEmail>();
		Parallel.ForEach(loadedEmails,
						 loadedEmail =>
						 {
							 var emailParser = new TextEmailParser(loadedEmail);
							 var parsedEmail = emailParser.Parse();
							 parsedEmails.Add(parsedEmail);
						 });

		return parsedEmails;
	}

	private async Task<IEnumerable<FilteredEmail>> FilterEmailsInParallel(IEnumerable<ParsedEmail> parsedEmails,
																		  IEnumerable<byte> selectedSpamFilters)
	{
		var filteredEmails = new List<FilteredEmail>();

		var tasks = parsedEmails.Select(async parsedEmail =>
		{
			var spamFilters = selectedSpamFilters.Select(spamFilterType => (SpamFilterType)spamFilterType)
												 .Select(spamFilterProvider.Create)
												 .ToList();
			var spamEmailFilter = new SpamEmailFilter(spamFilters, parsedEmail);
			var filteredEmail = await spamEmailFilter.FilterAsync();
			filteredEmails.Add(filteredEmail);
		});

		await Task.WhenAll(tasks);

		return filteredEmails;
	}
}