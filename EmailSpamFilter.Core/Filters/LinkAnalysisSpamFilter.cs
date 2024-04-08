namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Utilities;

/// <summary>
/// An <see cref="ISpamFilter"/> that checks for spam links in the email's body.
/// </summary>
public class LinkAnalysisSpamFilter : ISpamFilter
{
	private readonly ILinkExtractor linkExtractor;
	private readonly ILinkSafetyChecker linkSafetyChecker;

	/// <summary>
	/// Instantiates a new <see cref="LinkAnalysisSpamFilter"/> instance.
	/// </summary>
	/// <param name="linkExtractor">An instance of <see cref="ILinkExtractor"/> that the filter uses to extract links.</param>
	/// <param name="linkSafetyChecker">An instance of <see cref="ILinkSafetyChecker"/> that the filter uses to check if
	/// links are safe, potentially harmful, or malicious.</param>
	public LinkAnalysisSpamFilter(ILinkExtractor linkExtractor, ILinkSafetyChecker linkSafetyChecker)
	{
		this.linkExtractor = linkExtractor;
		this.linkSafetyChecker = linkSafetyChecker;
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