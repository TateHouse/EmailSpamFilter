namespace EmailSpamFilter.Core.Test.Filters;
using EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Models;
using FluentAssertions;

[TestFixture]
public class KeywordSignatureSpamFilterTest
{
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

		spamFilter = new KeywordSignatureSpamFilter(spamKeywords);
	}

	[Test]
	public void GivenEmailWithSubjectContainingSpamKeyword_WhenIsSpam_ThenReturnsTrue()
	{
		var email = new Email("Claim your free gift", "Hello, you have won a free gift.");
		var result = spamFilter.IsSpam(email);

		result.Should().BeTrue();
	}

	[Test]
	public void GivenEmailWithBodyContainingSpamKeyword_WhenIsSpam_ThenReturnsTrue()
	{
		var email = new Email("Important", "Buy now and get 50% off.");
		var result = spamFilter.IsSpam(email);

		result.Should().BeTrue();
	}

	[Test]
	public void GivenEmailWithoutSpamKeyword_WhenIsSpam_ThenReturnsFalse()
	{
		var email = new Email("Hello", "This is a test email.");
		var result = spamFilter.IsSpam(email);

		result.Should().BeFalse();
	}
}