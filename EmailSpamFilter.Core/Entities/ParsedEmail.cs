namespace EmailSpamFilter.Core.Entities;
/// <summary>
/// A class representing a parsed email with a file name, subject, and body.
/// </summary>
public class ParsedEmail
{
	public string FileName { get; }
	public string Subject { get; }
	public string Body { get; }

	/// <summary>
	/// Instantiates a new parsed email.
	/// </summary>
	/// <param name="fileName">The file name of the email, including the file extension.</param>
	/// <param name="subject">The subject of the email.</param>
	/// <param name="body">The body of the email.</param>
	public ParsedEmail(string fileName, string subject, string body)
	{
		FileName = fileName;
		Subject = subject;
		Body = body;
	}
}