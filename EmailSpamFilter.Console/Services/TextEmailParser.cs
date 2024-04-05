namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;

public class TextEmailParser : IEmailParser
{
	private readonly static char[] NewLine = new[] { '\n' };
	private readonly LoadedEmail loadedEmail;

	public TextEmailParser(LoadedEmail loadedEmail)
	{
		this.loadedEmail = loadedEmail;
	}

	public ParsedEmail Parse()
	{
		const byte minimumLines = 2;
		var lines = loadedEmail.Source.Split(TextEmailParser.NewLine, StringSplitOptions.None);

		if (lines.Length < minimumLines)
		{
			throw new FormatException("Source is not in the expected format.");
		}

		var subject = lines[0].Trim();
		var body = string.Join("\n", lines.Skip(1)).Trim();

		return new ParsedEmail(loadedEmail.FileName, subject, body);
	}
}