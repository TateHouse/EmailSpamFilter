namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Core.Entities;

public interface ISpamEmailFilter
{
	public Task<FilteredEmail> FilterAsync();
}