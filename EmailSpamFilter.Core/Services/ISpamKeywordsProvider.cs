namespace EmailSpamFilter.Console.Services;
public interface ISpamKeywordsProvider
{
	public IEnumerable<string> GetSpamKeywords();
}