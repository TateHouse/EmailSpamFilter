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
	public async Task GivenKeywordSignatureSpamFilterType_WhenCreateAsync_ThenReturnsKeywordSignatureSpamFilter()
	{
		var result = await spamFilterFactory.CreateAsync(SpamFilterType.KeywordSignature);

		result.Should().NotBeNull();
		result.Should().BeOfType<KeywordSignatureSpamFilter>();
	}

	[Test]
	public async Task GivenLinkAnalysisSpamFilterType_WhenCreateAsync_ThenReturnsLinkAnalysisSpamFilter()
	{
		var result = await spamFilterFactory.CreateAsync(SpamFilterType.LinkAnalysis);

		result.Should().NotBeNull();
		result.Should().BeOfType<LinkAnalysisSpamFilter>();
	}

	[Test]
	public async Task GivenUnsubscribeLinkSpamFilterType_WhenCreateAsync_ThenReturnsUnsubscribeLinkSpamFilter()
	{
		var result = await spamFilterFactory.CreateAsync(SpamFilterType.UnsubscribeLink);

		result.Should().NotBeNull();
		result.Should().BeOfType<UnsubscribeLinkSpamFilter>();
	}
}