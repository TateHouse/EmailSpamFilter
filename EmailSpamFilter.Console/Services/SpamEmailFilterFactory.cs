namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Core.Filters;

public class SpamEmailFilterFactory : ISpamEmailFilterFactory
{
	public ISpamEmailFilter Create(IEnumerable<ISpamFilter> spamFilters, ParsedEmail email)
	{
		return new SpamEmailFilter(spamFilters, email);
	}
}