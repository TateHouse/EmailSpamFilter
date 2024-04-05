namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Utilities;

public class SpamFilterFactory : ISpamFilterFactory
{
	private readonly IKeywordHasher keywordHasher;
	private readonly ILinkExtractor linkExtractor;
	private readonly ILinkSafetyChecker linkSafetyChecker;
	private readonly IEnumerable<string> spamKeywords;

	public SpamFilterFactory(IKeywordHasher keywordHasher,
							 ILinkExtractor linkExtractor,
							 ILinkSafetyChecker linkSafetyChecker,
							 IEnumerable<string> spamKeywords)
	{
		this.keywordHasher = keywordHasher;
		this.linkExtractor = linkExtractor;
		this.linkSafetyChecker = linkSafetyChecker;
		this.spamKeywords = spamKeywords;
	}

	public async Task<ISpamFilter> CreateAsync(SpamFilterType spamFilterType)
	{
		switch (spamFilterType)
		{
			case SpamFilterType.KeywordSignature:
				return await Task.FromResult(new KeywordSignatureSpamFilter(keywordHasher, spamKeywords));

			case SpamFilterType.LinkAnalysis:
				return await Task.FromResult(new LinkAnalysisSpamFilter(linkExtractor, linkSafetyChecker));

			case SpamFilterType.UnsubscribeLink:
				return await Task.FromResult(new UnsubscribeLinkSpamFilter(linkExtractor));

			default:
				throw new ArgumentException($"Invalid spam filter type: {spamFilterType}");
		}
	}
}