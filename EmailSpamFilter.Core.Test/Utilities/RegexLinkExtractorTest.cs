namespace EmailSpamFilter.Core.Test.Utilities;
using EmailSpamFilter.Core.Utilities;
using FluentAssertions;

[TestFixture]
public class RegexLinkExtractorTest
{
	private readonly ILinkExtractor linkExtractor = new RegexLinkExtractor();

	[Test]
	public void GivenTextWithLinks_WhenExtractLinks_ThenReturnsLinks()
	{
		var text = "This is a text with a link https://www.google.com and another link www.bing.com";
		var links = linkExtractor.ExtractLinks(text);

		links.Should().HaveCount(2);
		links.Should().Contain("https://www.google.com");
		links.Should().Contain("www.bing.com");
	}

	[Test]
	public void GivenTextWithoutLinks_WhenExtractLinks_ThenReturnsEmpty()
	{
		var text = "This is a text without links";
		var links = linkExtractor.ExtractLinks(text);

		links.Should().BeEmpty();
	}
}