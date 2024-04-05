namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Filters;
using System.Collections.ObjectModel;

public interface ISpamFilterProvider
{
	public ReadOnlyDictionary<byte, SpamFilterType> AvailableSpamFilterTypes { get; }

	public ISpamFilter Create(SpamFilterType spamFilterType);
}