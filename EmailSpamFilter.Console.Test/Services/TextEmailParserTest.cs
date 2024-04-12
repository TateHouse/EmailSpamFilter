namespace EmailSpamFilter.Console.Test.Services;
using EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Entities;
using FluentAssertions;

[TestFixture]
public class TextEmailParserTest
{
	private const string EmailName = "Email";

	[Test]
	public void GivenSourceWithOnlySubject_WhenParse_ThenThrowsFormatException()
	{
		var loadedEmail = new LoadedEmail(TextEmailParserTest.EmailName, "Subject");
		var emailParser = new TextEmailParser(loadedEmail);
		var action = () => emailParser.Parse();

		action.Should().Throw<FormatException>();
	}

	[Test]
	public void GivenSourceWithSubjectAndEmptyBody_WhenParse_ThenReturnsEmail()
	{
		var source = CreateSource(string.Empty);
		var loadedEmail = new LoadedEmail("Email", source);
		var emailParser = new TextEmailParser(loadedEmail);
		var email = emailParser.Parse();

		email.Subject.Should().Be("This is the subject line.");
		email.Body.Should().Be("");
	}

	[Test]
	public void GivenSourceWithSubjectAndBody_WhenParse_ThenReturnsEmail()
	{
		var source = CreateSource("This is the body of the email.");
		var loadedEmail = new LoadedEmail(TextEmailParserTest.EmailName, source);
		var emailParser = new TextEmailParser(loadedEmail);
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