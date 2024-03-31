namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Models;

public interface ISpamFilter
{
	public Task<bool> IsSpamAsync(Email email);
}