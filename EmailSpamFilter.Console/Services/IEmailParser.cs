namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;

/// <summary>
/// An interface for parsing emails.
/// </summary>
public interface IEmailParser
{
	/// <summary>
	/// Parses an email.
	/// </summary>
	/// <returns>A <see cref="ParsedEmail"/>.</returns>
	public ParsedEmail Parse();
}