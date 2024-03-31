namespace EmailSpamFilter.Core.Test.Filters;
using EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Models;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

[TestFixture]
public class LinkAnalysisSpamFilterTest
{
	private ISpamFilter spamFilter;

	[SetUp]
	public void SetUp()
	{
		var configuration = new ConfigurationBuilder().AddUserSecrets<LinkAnalysisSpamFilter>().Build();
		spamFilter = new LinkAnalysisSpamFilter(configuration);
	}

	[Test]
	public async Task GivenEmailWithUnsafeLink_WhenIsSpamAsync_ThenReturnsTrue()
	{
		var email = new Email("Subject", "http://malware.testing.google.test/testing/malware/");
		var result = await spamFilter.IsSpamAsync(email);

		result.Should().BeTrue();
	}

	[Test]
	public async Task GivenEmailWithSafeLink_WhenIsSpamAsync_ThenReturnsFalse()
	{
		var email = new Email("Subject", "https://www.google.com");
		var result = await spamFilter.IsSpamAsync(email);

		result.Should().BeFalse();
	}
}