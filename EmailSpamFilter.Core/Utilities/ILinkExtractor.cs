namespace EmailSpamFilter.Core.Utilities;
/// <summary>
/// An interface for extracting links from a string.
/// </summary>
public interface ILinkExtractor
{
	/// <summary>
	/// Extracts links from a string.
	/// </summary>
	/// <param name="text">The string to extract links from.</param>
	/// <returns>A collection of links extracted stored as strings from the string.</returns>
	public IEnumerable<string> ExtractLinks(string text);
}