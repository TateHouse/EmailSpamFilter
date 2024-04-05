namespace EmailSpamFilter.Console.Test.Services;
using EmailSpamFilter.Console.Services;
using FluentAssertions;

[TestFixture]
public class TextEmailParserTest
{
	[Test]
	public void GivenWhitespaceSource_WhenInstantiate_ThenThrowsArgumentException()
	{
		var action = () => new TextEmailParser(" ");

		action.Should().Throw<ArgumentException>();
	}

	[Test]
	public void GivenSourceWithOnlySubject_WhenParse_ThenThrowsFormatException()
	{
		var emailParser = new TextEmailParser("Subject");
		var action = () => emailParser.Parse();

		action.Should().Throw<FormatException>();
	}

	[Test]
	public void GivenSourceWithSubjectAndEmptyBody_WhenParse_ThenReturnsEmail()
	{
		var source = CreateSource(string.Empty);
		var emailParser = new TextEmailParser(source);
		var email = emailParser.Parse();

		email.Subject.Should().Be("This is the subject line.");
		email.Body.Should().Be("");
	}

	[Test]
	public void GivenSourceWithSubjectAndBody_WhenParse_ThenReturnsEmail()
	{
		var source = CreateSource("This is the body of the email.");
		var emailParser = new TextEmailParser(source);
		var email = emailParser.Parse();

		email.Subject.Should().Be("This is the subject line.");
		email.Body.Should().Be("This is the body of the email.");
	}

	private string CreateSource(string body)
	{
		const string subject = "This is the subject line.";

		return $"{subject}\n{body}";
	}
}