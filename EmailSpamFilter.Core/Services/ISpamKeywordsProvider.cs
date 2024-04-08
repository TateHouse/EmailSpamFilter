namespace EmailSpamFilter.Core.Services;
/// <summary>
/// An interface for providing spam keywords.
/// </summary>
public interface ISpamKeywordsProvider
{
	/// <summary>
	/// Gets the collection of spam keywords.
	/// </summary>
	/// <returns>An IEnumerable of spam keywords stored as strings.</returns>
	public IEnumerable<string> GetSpamKeywords();
}