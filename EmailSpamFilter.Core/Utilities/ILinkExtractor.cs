namespace EmailSpamFilter.Core.Utilities;
public interface ILinkExtractor
{
	public IEnumerable<string> ExtractLinks(string text);
}