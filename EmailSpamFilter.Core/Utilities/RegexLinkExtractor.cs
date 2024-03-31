namespace EmailSpamFilter.Core.Utilities;
using System.Text.RegularExpressions;

public class RegexLinkExtractor : ILinkExtractor
{
	public IEnumerable<string> ExtractLinks(string text)
	{
		const string pattern = @"^(https:|http:|www\.)\S*";
		var regex = new Regex(pattern, RegexOptions.IgnoreCase);

		return regex.Matches(text).Select(match => match.Value).ToList();
	}
}