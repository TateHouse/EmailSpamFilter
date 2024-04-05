namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;

public interface IEmailLoader
{
	public Task<IEnumerable<LoadedEmail>> LoadAsync();
}