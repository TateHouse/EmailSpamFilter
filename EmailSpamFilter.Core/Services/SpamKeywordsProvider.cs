namespace EmailSpamFilter.Console.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;

public class SpamKeywordsProvider : ISpamKeywordsProvider
{
	private readonly IConfiguration configuration;

	public SpamKeywordsProvider(IConfiguration configuration)
	{
		if (configuration == null || string.IsNullOrWhiteSpace(configuration["SpamKeywordsFile"]))
		{
			throw new ArgumentNullException(nameof(configuration),
											"The configuration must contain the SpamKeywords section");
		}

		this.configuration = configuration;
	}

	public IEnumerable<string> GetSpamKeywords()
	{
		var spamKeywordsPath = configuration["SpamKeywordsFile"];

		return new HashSet<string>(File.ReadAllLines(spamKeywordsPath)).ToImmutableHashSet();
	}
}