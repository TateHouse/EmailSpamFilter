namespace EmailSpamFilter.Console;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Utilities;
using EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Services;
using EmailSpamFilter.Core.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Program
{
	public async static Task Main(string[] args)
	{
		const string appSettingsPath = "AppSettings.json";
		const string secretsPath = "Secrets.json";

		var configurationBuilder = new ConfigurationBuilder();
		configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
		configurationBuilder.AddJsonFile(appSettingsPath, false, true);
		configurationBuilder.AddJsonFile(secretsPath, false, true);

		var configuration = configurationBuilder.Build();

		var serviceCollection = new ServiceCollection();
		serviceCollection.AddSingleton<IConfiguration>(configuration);
		serviceCollection.AddSingleton<IEmailLoaderFactory, TextEmailLoaderFactory>();
		serviceCollection.AddSingleton<IEmailParserFactory, TextEmailParserFactory>();
		serviceCollection.AddSingleton<IKeywordHasher, KeywordHasherSHA256>();
		serviceCollection.AddSingleton<ILinkExtractor, RegexLinkExtractor>();
		serviceCollection.AddSingleton<ISpamFilterProvider, SpamFilterProvider>();
		serviceCollection.AddSingleton<ISpamFilterFactory, SpamFilterFactory>();
		serviceCollection.AddSingleton<ISpamEmailFilterFactory, SpamEmailFilterFactory>();
		serviceCollection.AddSingleton<IConsoleUserInterface, ConsoleUserInterface>();
		serviceCollection.AddSingleton<IConsoleStringBuilder<FilteredEmail>, FilteredEmailStringBuilder>();

		serviceCollection.AddSingleton<Application>();

		serviceCollection.AddTransient<ILinkSafetyChecker, GoogleSafeBrowsingLinkSafetyChecker>();
		serviceCollection.AddTransient<ISpamKeywordsProvider, SpamKeywordsProvider>();

		var serviceProvider = serviceCollection.BuildServiceProvider();

		var application = serviceProvider.GetRequiredService<Application>();

		await application.Run();
	}
}