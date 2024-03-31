namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Models;

public interface ISpamFilter
{
	public bool IsSpam(Email email);
}