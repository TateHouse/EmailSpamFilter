namespace EmailSpamFilter.Core.Filters;
public interface ISpamFilterFactory
{
	public ISpamFilter Create(SpamFilterType spamFilterType);
}