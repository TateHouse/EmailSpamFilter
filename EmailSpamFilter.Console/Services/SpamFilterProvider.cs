namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Filters;
using System.Collections.ObjectModel;

public class SpamFilterProvider : ISpamFilterProvider
{
	public ReadOnlyDictionary<byte, SpamFilterType> AvailableSpamFilterTypes { get; } =
		new ReadOnlyDictionary<byte, SpamFilterType>(new Dictionary<byte, SpamFilterType>
		{
			{ 1, SpamFilterType.KeywordSignature },
			{ 2, SpamFilterType.LinkAnalysis },
			{ 3, SpamFilterType.UnsubscribeLink }
		});

	private readonly ISpamFilterFactory spamFilterFactory;

	public SpamFilterProvider(ISpamFilterFactory spamFilterFactory)
	{
		this.spamFilterFactory = spamFilterFactory;
	}

	public ISpamFilter Create(SpamFilterType spamFilterType)
	{
		return spamFilterFactory.Create(spamFilterType);
	}
}