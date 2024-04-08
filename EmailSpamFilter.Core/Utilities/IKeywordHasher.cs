namespace EmailSpamFilter.Core.Utilities;
/// <summary>
/// An interface for hashing keywords.
/// </summary>
public interface IKeywordHasher
{
	/// <summary>
	/// Hashes a keyword.
	/// </summary>
	/// <param name="keyword">The keyword to hash.</param>
	/// <returns>The hashed keyword's string representation.</returns>
	public string HashKeyword(string keyword);
}