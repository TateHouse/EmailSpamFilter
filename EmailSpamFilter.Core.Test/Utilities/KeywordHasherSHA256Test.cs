namespace EmailSpamFilter.Core.Test.Utilities;
using EmailSpamFilter.Core.Utilities;
using FluentAssertions;

[TestFixture]
public class KeywordHasherSHA256Test
{
	private readonly IKeywordHasher keywordHasher = new KeywordHasherSHA256();

	[Test]
	public void GivenKeyword_WhenHashKeyword_ThenReturnsSameHashForSameKeyword()
	{
		const string keyword = "Free";
		var firstHash = keywordHasher.HashKeyword(keyword);
		var secondHash = keywordHasher.HashKeyword(keyword);

		firstHash.Should().Be(secondHash);
	}

	[Test]
	public void GivenKeyword_WhenHashKeyword_ThenReturnsHashOfLowercasedKeyword()
	{
		const string uppercase = "Free";
		var lowercase = uppercase.ToLowerInvariant();

		var actual = keywordHasher.HashKeyword(uppercase);
		var expected = keywordHasher.HashKeyword(lowercase);

		actual.Should().Be(expected);
	}
}