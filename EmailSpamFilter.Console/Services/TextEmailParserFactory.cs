namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Services;

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