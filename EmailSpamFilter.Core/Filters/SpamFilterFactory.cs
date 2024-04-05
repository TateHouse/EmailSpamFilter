namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Utilities;

public class SpamFilterFactory : ISpamFilterFactory
{
	private readonly IKeywordHasher keywordHasher;
	private readonly ILinkExtractor linkExtractor;
	private readonly ILinkSafetyChecker linkSafetyChecker;
	private readonly ISpamKeywordsProvider spamKeywordsProvider;

	public SpamFilterFactory(IKeywordHasher keywordHasher,
							 ILinkExtractor linkExtractor,
							 ILinkSafetyChecker linkSafetyChecker,
							 ISpamKeywordsProvider spamKeywordsProvider)
	{
		this.keywordHasher = keywordHasher;
		this.linkExtractor = linkExtractor;
		this.linkSafetyChecker = linkSafetyChecker;
		this.spamKeywordsProvider = spamKeywordsProvider;
	}

	public ISpamFilter Create(SpamFilterType spamFilterType)
	{
		switch (spamFilterType)
		{
			case SpamFilterType.KeywordSignature:
				var spamKeywords = spamKeywordsProvider.GetSpamKeywords();

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