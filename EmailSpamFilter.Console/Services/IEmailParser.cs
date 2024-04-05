namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Entities;

public interface IEmailParser
{
	public Email Parse();
}