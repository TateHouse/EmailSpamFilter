namespace EmailSpamFilter.Core.Test.Services;
using EmailSpamFilter.Core.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Collections.Immutable;

[TestFixture]
public class SpamKeywordsProviderTest
{
	private readonly IEnumerable<string> spamKeywords = new List<string>
	{
		"Free",
		"Money",
		"Buy",
		"Claim",
		"Gift",
		"Deal"
	};

	private Mock<IConfiguration> mockConfiguration;
	private ISpamKeywordsProvider spamKeywordsProvider;

	[SetUp]
	public void SetUp()
	{

		mockConfiguration = new Mock<IConfiguration>();
		mockConfiguration.Setup(mock => mock["SpamKeywordsFile"])
						 .Returns("EmailSpamFilter.Core.Test/Services/SpamKeywordsProviderTest/SpamKeywords.txt");

		Directory.CreateDirectory("EmailSpamFilter.Core.Test/Services/SpamKeywordsProviderTest");
		File.WriteAllLines(mockConfiguration.Object["SpamKeywordsFile"]!, spamKeywords);

		spamKeywordsProvider = new SpamKeywordsProvider(mockConfiguration.Object);
	}

	[TearDown]
	public void TearDown()
	{
		Directory.Delete("EmailSpamFilter.Core.Test/Services/SpamKeywordsProviderTest", true);
	}

	[Test]
	public void GivenSpamKeywordsFile_WhenGetSpamKeywords_ThenReturnsSpamKeywordsTrimmedAndLowercased()
	{
		var expected = spamKeywords.Select(keyword => keyword.Trim().ToLowerInvariant()).ToImmutableHashSet();
		var result = spamKeywordsProvider.GetSpamKeywords();

		result.Should().BeEquivalentTo(expected);
	}
}