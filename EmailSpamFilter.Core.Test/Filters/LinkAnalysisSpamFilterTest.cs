namespace EmailSpamFilter.Core.Test.Filters;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Utilities;
using FluentAssertions;
using Moq;

[TestFixture]
public class LinkAnalysisSpamFilterTest
{
	private Mock<ILinkExtractor> mockLinkExtractor;
	private Mock<ILinkSafetyChecker> mockLinkSafetyChecker;
	private ISpamFilter spamFilter;

	[SetUp]
	public void SetUp()
	{
		mockLinkExtractor = new Mock<ILinkExtractor>();
		mockLinkSafetyChecker = new Mock<ILinkSafetyChecker>();
		spamFilter = new LinkAnalysisSpamFilter(mockLinkExtractor.Object, mockLinkSafetyChecker.Object);
	}

	[Test]
	public async Task GivenEmailWithUnsafeLink_WhenIsSpamAsync_ThenReturnsTrue()
	{
		var email = new Email("Subject", "Body with unsafe link http://unsafe-link.com");
		mockLinkExtractor.Setup(mock => mock.ExtractLinks(It.IsAny<string>()))
						 .Returns(new List<string> { "http://unsafe-link.com" });
		mockLinkSafetyChecker.Setup(mock => mock.IsLinkUnsafeAsync(It.IsAny<string>())).ReturnsAsync(true);

		var result = await spamFilter.IsSpamAsync(email);

		result.Should().BeTrue();
	}

	[Test]
	public async Task GivenEmailWithSafeLink_WhenIsSpamAsync_ThenReturnsFalse()
	{
		var email = new Email("Subject", "https://safe-link.com");
		mockLinkExtractor.Setup(mock => mock.ExtractLinks(It.IsAny<string>()))
						 .Returns(new List<string> { "https://safe-link.com" });
		mockLinkSafetyChecker.Setup(mock => mock.IsLinkUnsafeAsync(It.IsAny<string>())).ReturnsAsync(false);

		var result = await spamFilter.IsSpamAsync(email);

		result.Should().BeFalse();
	}

	[Test]
	public async Task GivenEmalWithNoLinks_WhenIsSpamAsync_ThenReturnsFalse()
	{
		var email = new Email("Subject", "Body with no links");
		mockLinkExtractor.Setup(mock => mock.ExtractLinks(It.IsAny<string>())).Returns(new List<string>());

		var result = await spamFilter.IsSpamAsync(email);

		result.Should().BeFalse();
	}
}