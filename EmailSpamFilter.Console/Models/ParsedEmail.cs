namespace EmailSpamFilter.Console.Models;
public class ParsedEmail
{
	public string FileName { get; }
	public string Subject { get; }
	public string Body { get; }

	public string Content
	{
		get
		{
			return $"{Subject}{Environment.NewLine}{Body}";
		}
	}

	public ParsedEmail(string fileName, string subject, string body)
	{
		FileName = fileName;
		Subject = subject;
		Body = body;
	}
}