namespace EmailSpamFilter.Core.Filters;
using System.Security.Cryptography;
using System.Text;
using EmailSpamFilter.Core.Models;

public class KeywordSignatureSpamFilter : ISpamFilter
{
	private readonly HashSet<string> spamSignatures = new HashSet<string>();

	public KeywordSignatureSpamFilter(IEnumerable<string> spamKeywords)
	{
		foreach (var keyword in spamKeywords)
		{
			var hash = KeywordSignatureSpamFilter.HashKeyword(keyword);
			spamSignatures.Add(hash);
		}
	}

	public bool IsSpam(Email email)
	{
		var subjectWords = email.Subject.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
		var bodyWords = email.Body.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

		return subjectWords.Exists(IsKeywordSpam) || bodyWords.Exists(IsKeywordSpam);
	}

	private static string HashKeyword(string keyword)
	{
		var formatted = keyword.Trim().ToLowerInvariant();
		var bytes = Encoding.UTF8.GetBytes(formatted);
		var hash = SHA256.HashData(bytes);

		return Convert.ToBase64String(hash);
	}

	private bool IsKeywordSpam(string keyword)
	{
		var hash = KeywordSignatureSpamFilter.HashKeyword(keyword);

		return spamSignatures.Contains(hash);
	}
}