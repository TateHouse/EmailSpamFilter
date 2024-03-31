namespace EmailSpamFilter.Core.Models;
public class Email
{
	public string Subject { get; private set; }
	public string Body { get; private set; }

	public Email(string subject, string body)
	{
		Subject = subject;
		Body = body;
	}
}