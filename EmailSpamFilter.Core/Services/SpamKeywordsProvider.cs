namespace EmailSpamFilter.Console.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Immutable;

/// <summary>
/// A provider for spam keywords stored in a text file on disk where each spam keyword is on a separate line.
/// </summary>
public class SpamKeywordsProvider : ISpamKeywordsProvider
{
	private readonly string path;

	/// <summary>
	/// Instantiates a new <see cref="SpamKeywordsProvider"/> instance.
	/// </summary>
	/// <param name="configuration">An instance of <see cref="IConfiguration"/> that contains the spam keywords file
	/// path.</param>
	/// <exception cref="ArgumentException">Thrown if the SpamKeywordsFile configuration value is missing.</exception>
	/// <exception cref="FileNotFoundException">Thrown if the spam keywords file was not found at the provided path.</exception>
	public SpamKeywordsProvider(IConfiguration configuration)
	{
		const string key = "SpamKeywordsFile";
		var path = configuration[key];

		if (string.IsNullOrWhiteSpace(path))
		{
			throw new ArgumentException("The SpamKeywordsFile configuration value is missing.", nameof(configuration));
		}

		if (!File.Exists(path))
		{
			throw new FileNotFoundException("The provided spam keywords file does not exist.");
		}

		this.path = path;
	}

	public IEnumerable<string> GetSpamKeywords()
	{
		var source = File.ReadAllLines(path);

		return source.Select(keyword => keyword.Trim().ToLowerInvariant()).ToImmutableHashSet();
	}
}