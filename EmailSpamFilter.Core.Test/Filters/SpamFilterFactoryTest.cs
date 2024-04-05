namespace EmailSpamFilter.Core.Test.Filters;
using EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Utilities;
using FluentAssertions;
using Moq;

[TestFixture]
public class SpamFilterFactoryTest
{
	private ISpamFilterFactory spamFilterFactory;
	private Mock<IKeywordHasher> mockKeywordHasher;
	private Mock<ILinkExtractor> mockLinkExtractor;
	private Mock<ILinkSafetyChecker> mockLinkSafetyChecker;

	[SetUp]
	public void SetUp()
	{
		mockKeywordHasher = new Mock<IKeywordHasher>();
		mockLinkExtractor = new Mock<ILinkExtractor>();
		mockLinkSafetyChecker = new Mock<ILinkSafetyChecker>();
		spamFilterFactory = new SpamFilterFactory(mockKeywordHasher.Object,
												  mockLinkExtractor.Object,
												  mockLinkSafetyChecker.Object,
												  new List<string>());
	}

	[Test]
	public void GivenKeywordSignatureSpamFilterType_WhenCreate_ThenReturnsKeywordSignatureSpamFilter()
	{
		var result = spamFilterFactory.Create(SpamFilterType.KeywordSignature);

		result.Should().NotBeNull();
		result.Should().BeOfType<KeywordSignatureSpamFilter>();
	}

	[Test]
	public void GivenLinkAnalysisSpamFilterType_WhenCreate_ThenReturnsLinkAnalysisSpamFilter()
	{
		var result = spamFilterFactory.Create(SpamFilterType.LinkAnalysis);

		result.Should().NotBeNull();
		result.Should().BeOfType<LinkAnalysisSpamFilter>();
	}

	[Test]
	public void GivenUnsubscribeLinkSpamFilterType_WhenCreate_ThenReturnsUnsubscribeLinkSpamFilter()
	{
		var result = spamFilterFactory.Create(SpamFilterType.UnsubscribeLink);

		result.Should().NotBeNull();
		result.Should().BeOfType<UnsubscribeLinkSpamFilter>();
	}
}