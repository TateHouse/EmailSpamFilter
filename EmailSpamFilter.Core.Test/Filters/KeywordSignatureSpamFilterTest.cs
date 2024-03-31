namespace EmailSpamFilter.Core.Test.Filters;
using EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Models;
using EmailSpamFilter.Core.Utilities;
using FluentAssertions;

[TestFixture]
public class KeywordSignatureSpamFilterTest
{
	private readonly IKeywordHasher keywordHasher = new KeywordHasherSHA256();
	private ISpamFilter spamFilter;

	[SetUp]
	public void SetUp()
	{

		var spamKeywords = new List<string>
		{
			"Free",
			"Money",
			"Buy",
			"Claim",
			"Gift",
			"Deal"
		};

		spamFilter = new KeywordSignatureSpamFilter(keywordHasher, spamKeywords);
	}

	[Test]
	public async Task GivenEmailWithSubjectContainingSpamKeyword_WhenIsSpamAsync_ThenReturnsTrue()
	{
		var email = new Email("Claim your free gift", "Hello, you have won a free gift.");
		var result = await spamFilter.IsSpamAsync(email);

		result.Should().BeTrue();
	}

	[Test]
	public async Task GivenEmailWithBodyContainingSpamKeyword_WhenIsSpamAsync_ThenReturnsTrue()
	{
		var email = new Email("Important", "Buy now and get 50% off.");
		var result = await spamFilter.IsSpamAsync(email);

		result.Should().BeTrue();
	}

	[Test]
	public async Task GivenEmailWithoutSpamKeyword_WhenIsSpamAsync_ThenReturnsFalse()
	{
		var email = new Email("Hello", "This is a test email.");
		var result = await spamFilter.IsSpamAsync(email);

		result.Should().BeFalse();
	}
}