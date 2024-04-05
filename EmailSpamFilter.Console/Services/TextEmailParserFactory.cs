namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;

public class TextEmailParserFactory : IEmailParserFactory
{
	public IEmailParser Create(LoadedEmail email)
	{
		return new TextEmailParser(email);
	}
}