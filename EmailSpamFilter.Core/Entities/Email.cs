namespace EmailSpamFilter.Core.Entities;
/// <summary>
/// This class represents an email with a subject and a body.
/// </summary>
public class Email
{
	public string Subject { get; private set; }
	public string Body { get; private set; }

	/// <summary>
	/// Instantiates a new <see cref="Email"/> instance.
	/// </summary>
	/// <param name="subject">The subject of the email.</param>
	/// <param name="body">The body of the email.</param>
	public Email(string subject, string body)
	{
		Subject = subject;
		Body = body;
	}
}