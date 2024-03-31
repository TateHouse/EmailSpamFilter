namespace EmailSpamFilter.Core.Test.Filters;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Utilities;
using FluentAssertions;
using Moq;

[TestFixture]
public class UnsubscribeLinkSpamFilterTest
{
	private Mock<ILinkExtractor> mockLinkExtractor;
	private ISpamFilter spamFilter;

	[SetUp]
	public void SetUp()
	{
		mockLinkExtractor = new Mock<ILinkExtractor>();
		spamFilter = new UnsubscribeLinkSpamFilter(mockLinkExtractor.Object);
	}

	[Test]
	public void GivenEmailWithoutUnsubscribeLink_WhenIsSpamAsync_ThenReturnsTrue()
	{
		var email = new Email("Subject", "This is a test email without unsubscribe link");
		mockLinkExtractor.Setup(mock => mock.ExtractLinks(It.IsAny<string>())).Returns(new List<string>());
		var result = spamFilter.IsSpamAsync(email).Result;

		result.Should().BeTrue();
	}

	[Test]
	public void GivenEmailWithUnsubscribeLink_WhenIsSpamAsync_ThenReturnsFalse()
	{
		var email = new Email("Subject",
							  "This is a test email with a link https://example.com/unsubscribe for unsubscribing.");
		mockLinkExtractor.Setup(mock => mock.ExtractLinks(It.IsAny<string>()))
						 .Returns(new List<string> { "https://example.com/unsubscribe" });
		var result = spamFilter.IsSpamAsync(email).Result;

		result.Should().BeFalse();
	}
}