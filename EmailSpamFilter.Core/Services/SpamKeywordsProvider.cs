namespace EmailSpamFilter.Console.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;

public class SpamKeywordsProvider : ISpamKeywordsProvider
{
	private readonly string path;

	public SpamKeywordsProvider(IConfiguration configuration)
	{
		const string key = "SpamKeywordsFile";
		var path = configuration[key];

		if (string.IsNullOrWhiteSpace(path))
		{
			throw new ArgumentNullException(nameof(configuration),
											"The configuration must contain the SpamKeywords section");
		}

		if (!File.Exists(path))
		{
			throw new FileNotFoundException("The provided spam keywords file does not exist.");
		}

		this.path = path;
	}

	public IEnumerable<string> GetSpamKeywords()
	{
		return new HashSet<string>(File.ReadAllLines(path)).ToImmutableHashSet();
	}
}