namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Services;

/// <summary>
/// A class for parsing loaded emails.
/// </summary>
public class TextEmailParser : IEmailParser
{
	private readonly static char[] NewLine = new[] { '\n' };
	private readonly LoadedEmail email;

	/// <summary>
	/// Instantiates a new <see cref="TextEmailParser"/> instance.
	/// </summary>
	/// <param name="email">The <see cref="LoadedEmail"/> to parse.</param>
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