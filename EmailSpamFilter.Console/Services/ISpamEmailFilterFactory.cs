namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Core.Filters;

public interface ISpamEmailFilterFactory
{
	public ISpamEmailFilter Create(IEnumerable<ISpamFilter> spamFilters, ParsedEmail email);
}