namespace EmailSpamFilter.Core.Utilities;
using System.Text.RegularExpressions;

/// <summary>
/// A class for extracting links from a string using a regular expression.
/// </summary>
public class RegexLinkExtractor : ILinkExtractor
{
	public IEnumerable<string> ExtractLinks(string text)
	{
		const string pattern = @"(https:|http:|www\.)\S*";
		var regex = new Regex(pattern, RegexOptions.IgnoreCase);

		return regex.Matches(text).Select(match => match.Value).ToList();
	}
}