namespace EmailSpamFilter.Core.Utilities;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// A class for hashing keywords using the SHA-256 algorithm and encoding the result as a base64 string.
/// </summary>
public class KeywordHasherSHA256 : IKeywordHasher
{
	public string HashKeyword(string keyword)
	{
		var formatted = keyword.Trim().ToLowerInvariant();
		var bytes = Encoding.UTF8.GetBytes(formatted);
		var hash = SHA256.HashData(bytes);

		return Convert.ToBase64String(hash);
	}
}