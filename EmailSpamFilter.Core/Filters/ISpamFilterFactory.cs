namespace EmailSpamFilter.Core.Filters;
public interface ISpamFilterFactory
{
	public Task<ISpamFilter> CreateAsync(SpamFilterType spamFilterType);
}