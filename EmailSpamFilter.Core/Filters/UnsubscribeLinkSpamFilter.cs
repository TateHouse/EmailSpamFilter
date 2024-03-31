namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Models;
using EmailSpamFilter.Core.Utilities;

public class UnsubscribeLinkSpamFilter : ISpamFilter
{
	private readonly ILinkExtractor linkExtractor;

	public UnsubscribeLinkSpamFilter(ILinkExtractor linkExtractor)
	{
		this.linkExtractor = linkExtractor ?? throw new ArgumentNullException(nameof(linkExtractor));
	}

	public Task<bool> IsSpamAsync(Email email)
	{
		var links = linkExtractor.ExtractLinks(email.Body);
		var unsubscribeLinks = links.Any(link => link.ToLowerInvariant().Contains("unsubscribe"));

		return Task.FromResult(!unsubscribeLinks);
	}
}