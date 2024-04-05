namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Entities;

public class TextEmailParser : IEmailParser
{
	private readonly static char[] NewLine = new[] { '\n' };
	private readonly string source;

	public TextEmailParser(string source)
	{
		if (string.IsNullOrWhiteSpace(source))
		{
			throw new ArgumentException("Value cannot be null or whitespace.", nameof(source));
		}

		this.source = source;
	}

	public Email Parse()
	{
		const byte minimumLines = 2;
		var lines = source.Split(TextEmailParser.NewLine, StringSplitOptions.None);

		if (lines.Length < minimumLines)
		{
			throw new FormatException("Source is not in the expected format.");
		}

		var subject = lines[0].Trim();
		var body = string.Join("\n", lines.Skip(1)).Trim();

		return new Email(subject, body);
	}
}