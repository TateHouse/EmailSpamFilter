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

	public ISpamFilter Create(SpamFilterType spamFilterType)
	{
		switch (spamFilterType)
		{
			case SpamFilterType.KeywordSignature:
				return new KeywordSignatureSpamFilter(keywordHasher, spamKeywords);

			case SpamFilterType.LinkAnalysis:
				return new LinkAnalysisSpamFilter(linkExtractor, linkSafetyChecker);

			case SpamFilterType.UnsubscribeLink:
				return new UnsubscribeLinkSpamFilter(linkExtractor);

			default:
				throw new ArgumentException($"Invalid spam filter type: {spamFilterType}");
		}
	}
}