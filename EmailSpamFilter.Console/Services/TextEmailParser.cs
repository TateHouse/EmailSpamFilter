namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;

public class TextEmailParser : IEmailParser
{
	private readonly static char[] NewLine = new[] { '\n' };
	private readonly LoadedEmail email;

	public TextEmailParser(LoadedEmail email)
	{
		this.email = email;
	}

	public ParsedEmail Parse()
	{
		const byte minimumLines = 2;
		var lines = email.Source.Split(TextEmailParser.NewLine, StringSplitOptions.None);

		if (lines.Length < minimumLines)
		{
			throw new FormatException("Source is not in the expected format.");
		}

		var subject = lines[0].Trim();
		var body = string.Join("\n", lines.Skip(1)).Trim();

		return new ParsedEmail(email.FileName, subject, body);
	}
}