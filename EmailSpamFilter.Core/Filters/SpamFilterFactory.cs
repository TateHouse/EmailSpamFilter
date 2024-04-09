namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Services;
using EmailSpamFilter.Core.Utilities;

/// <summary>
/// A factory for creating <see cref="ISpamFilter"/>.
/// </summary>
public class SpamFilterFactory : ISpamFilterFactory
{
	private readonly IKeywordHasher keywordHasher;
	private readonly ILinkExtractor linkExtractor;
	private readonly ILinkSafetyChecker linkSafetyChecker;
	private readonly ISpamKeywordsProvider spamKeywordsProvider;

	/// <summary>
	/// Instantiates a new <see cref="SpamFilterFactory"/> instance.
	/// </summary>
	/// <param name="keywordHasher">An instance of <see cref="IKeywordHasher"/>.</param>
	/// <param name="linkExtractor">An instance of <see cref="ILinkExtractor"/>.</param>
	/// <param name="linkSafetyChecker">An instance of <see cref="ILinkSafetyChecker"/>.</param>
	/// <param name="spamKeywordsProvider">An instance of <see cref="ISpamKeywordsProvider"/> that the factory uses to
	/// get the collection of spam keywords.</param>
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