namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Utilities;

public class LinkAnalysisSpamFilter : ISpamFilter
{
	private readonly ILinkExtractor linkExtractor;
	private readonly ILinkSafetyChecker linkSafetyChecker;

	public LinkAnalysisSpamFilter(ILinkExtractor linkExtractor, ILinkSafetyChecker linkSafetyChecker)
	{
		this.linkExtractor = linkExtractor ?? throw new ArgumentNullException(nameof(linkExtractor));
		this.linkSafetyChecker = linkSafetyChecker ?? throw new ArgumentNullException(nameof(linkSafetyChecker));
	}

	public async Task<bool> IsSpamAsync(Email email)
	{
		var links = linkExtractor.ExtractLinks(email.Body);

		foreach (var link in links)
		{
			var isUnsafe = await linkSafetyChecker.IsLinkUnsafeAsync(link);

			if (isUnsafe)
			{
				return true;
			}
		}

		return false;
	}
}