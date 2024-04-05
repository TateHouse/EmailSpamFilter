namespace EmailSpamFilter.Core.Test.Filters;
using EmailSpamFilter.Console.Services;
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
	private Mock<ISpamKeywordsProvider> mockSpamKeywordsProvider;

	[SetUp]
	public void SetUp()
	{
		mockKeywordHasher = new Mock<IKeywordHasher>();
		mockLinkExtractor = new Mock<ILinkExtractor>();
		mockLinkSafetyChecker = new Mock<ILinkSafetyChecker>();
		mockSpamKeywordsProvider = new Mock<ISpamKeywordsProvider>();
		spamFilterFactory = new SpamFilterFactory(mockKeywordHasher.Object,
												  mockLinkExtractor.Object,
												  mockLinkSafetyChecker.Object,
												  mockSpamKeywordsProvider.Object);
	}

	[Test]
	public void GivenKeywordSignatureSpamFilterType_WhenCreateAsync_ThenReturnsKeywordSignatureSpamFilter()
	{
		var result = spamFilterFactory.Create(SpamFilterType.KeywordSignature);

		result.Should().NotBeNull();
		result.Should().BeOfType<KeywordSignatureSpamFilter>();
	}

	[Test]
	public void GivenLinkAnalysisSpamFilterType_WhenCreateAsync_ThenReturnsLinkAnalysisSpamFilter()
	{
		var result = spamFilterFactory.Create(SpamFilterType.LinkAnalysis);

		result.Should().NotBeNull();
		result.Should().BeOfType<LinkAnalysisSpamFilter>();
	}

	[Test]
	public void GivenUnsubscribeLinkSpamFilterType_WhenCreateAsync_ThenReturnsUnsubscribeLinkSpamFilter()
	{
		var result = spamFilterFactory.Create(SpamFilterType.UnsubscribeLink);

		result.Should().NotBeNull();
		result.Should().BeOfType<UnsubscribeLinkSpamFilter>();
	}
}