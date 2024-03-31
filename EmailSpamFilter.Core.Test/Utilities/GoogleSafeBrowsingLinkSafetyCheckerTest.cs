namespace EmailSpamFilter.Core.Test.Utilities;
using EmailSpamFilter.Core.Utilities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

[TestFixture]
public class GoogleSafeBrowsingLinkSafetyCheckerTest
{
	private ILinkSafetyChecker linkSafetyChecker;

	[SetUp]
	public void SetUp()
	{
		var configuration = new ConfigurationBuilder().AddUserSecrets<GoogleSafeBrowsingLinkSafetyChecker>().Build();
		linkSafetyChecker = new GoogleSafeBrowsingLinkSafetyChecker(configuration);
	}

	[Test]
	public async Task GivenUnsafeLink_WhenIsLinkUnsafeAsync_ThenReturnsTrue()
	{
		var link = "http://malware.testing.google.test/testing/malware/";
		var result = await linkSafetyChecker.IsLinkUnsafeAsync(link);

		result.Should().BeTrue();
	}

	[Test]
	public async Task GivenSafeLink_WhenIsLinkUnsafeAsync_ThenReturnsFalse()
	{
		var link = "https://www.google.com";
		var result = await linkSafetyChecker.IsLinkUnsafeAsync(link);

		result.Should().BeFalse();
	}
}