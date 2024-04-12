namespace EmailSpamFilter.Core.Services;
using EmailSpamFilter.Core.Entities;

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