namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Models;
using EmailSpamFilter.Core.Utilities;

public class KeywordSignatureSpamFilter : ISpamFilter
{
	private readonly IKeywordHasher keywordHasher;
	private readonly HashSet<string> spamSignatures = new HashSet<string>();

	public KeywordSignatureSpamFilter(IKeywordHasher keywordHasher, IEnumerable<string> spamKeywords)
	{
		this.keywordHasher = keywordHasher ?? throw new ArgumentNullException(nameof(keywordHasher));

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

	private bool IsKeywordSpam(string keyword)
	{
		var hash = keywordHasher.HashKeyword(keyword);

		return spamSignatures.Contains(hash);
	}
}