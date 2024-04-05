namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;

public interface IEmailParserFactory
{
	public IEmailParser Create(LoadedEmail email);
}