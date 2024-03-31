namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Entities;

public interface ISpamFilter
{
	public Task<bool> IsSpamAsync(Email email);
}