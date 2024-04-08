namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Utilities;

/// <summary>
/// An <see cref="ISpamFilter"/> that checks for spam links in the <see cref="Email"/> body.
/// </summary>
public class UnsubscribeLinkSpamFilter : ISpamFilter
{
	private readonly ILinkExtractor linkExtractor;

	/// <summary>
	/// Instantiates a new <see cref="UnsubscribeLinkSpamFilter"/> instance.
	/// </summary>
	/// <param name="linkExtractor">An instance of <see cref="ILinkExtractor"/> that the filter uses to extract links.</param>
	public UnsubscribeLinkSpamFilter(ILinkExtractor linkExtractor)
	{
		this.linkExtractor = linkExtractor;
	}

	public Task<bool> IsSpamAsync(Email email)
	{
		var links = linkExtractor.ExtractLinks(email.Body);
		var unsubscribeLinks = links.Any(link => link.ToLowerInvariant().Contains("unsubscribe"));

		return Task.FromResult(!unsubscribeLinks);
	}
}