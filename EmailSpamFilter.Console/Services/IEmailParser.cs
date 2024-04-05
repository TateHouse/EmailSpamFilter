namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;

public interface IEmailParser
{
	public ParsedEmail Parse();
}