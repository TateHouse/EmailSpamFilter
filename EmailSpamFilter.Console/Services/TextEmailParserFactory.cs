namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;

/// <summary>
/// A factory for creating <see cref="IEmailParser"/> instances.
/// </summary>
public class TextEmailParserFactory : IEmailParserFactory
{
	public IEmailParser Create(LoadedEmail email)
	{
		return new TextEmailParser(email);
	}
}