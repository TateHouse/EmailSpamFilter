namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Utilities;

/// <summary>
/// An <see cref="ISpamFilter"/> that checks for spam keywords in the email's subject and body.
/// </summary>
public class KeywordSignatureSpamFilter : ISpamFilter
{
	private readonly IKeywordHasher keywordHasher;
	private readonly HashSet<string> spamSignatures = new HashSet<string>();

	/// <summary>
	/// Instantiates a new <see cref="KeywordSignatureSpamFilter"/>.
	/// </summary>
	/// <param name="keywordHasher">An instance of <see cref="IKeywordHasher"/> that the filter uses to hash keywords.</param>
	/// <param name="spamKeywords">A collection of spam keywords to be hashed and stored for spam checking.</param>
	public KeywordSignatureSpamFilter(IKeywordHasher keywordHasher, IEnumerable<string> spamKeywords)
	{
		this.keywordHasher = keywordHasher;

		foreach (var keyword in spamKeywords)
		{
			var hash = keywordHasher.HashKeyword(keyword);
			spamSignatures.Add(hash);
		}
	}

	public Task<bool> IsSpamAsync(Email email)
	{
		var subjectWords = email.Subject.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
		var bodyWords = email.Body.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
		var isSpam = subjectWords.Exists(IsKeywordSpam) || bodyWords.Exists(IsKeywordSpam);

		return Task.FromResult(isSpam);
	}

	/// <summary>
	/// Hashes the keyword and checks if it is stored in <see cref="spamSignatures"/>.
	/// </summary>
	/// <param name="keyword">The keyword to check.</param>
	/// <returns>True if the keyword's hash is stored in the <see cref="spamSignatures"/>. Otherwise, false.</returns>
	private bool IsKeywordSpam(string keyword)
	{
		var hash = keywordHasher.HashKeyword(keyword);

		return spamSignatures.Contains(hash);
	}
}