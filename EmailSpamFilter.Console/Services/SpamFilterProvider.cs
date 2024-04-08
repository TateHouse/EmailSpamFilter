namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Filters;
using System.Collections.ObjectModel;

/// <summary>
/// A provider for <see cref="ISpamFilter"/> instances.
/// </summary>
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

	/// <summary>
	/// Instantiates a new <see cref="ISpamFilterFactory"/>.
	/// </summary>
	/// <param name="spamFilterFactory">An instance of <see cref="ISpamFilterFactory"/> used to instantiate
	/// <see cref="ISpamFilter"/>.</param>
	public SpamFilterProvider(ISpamFilterFactory spamFilterFactory)
	{
		this.spamFilterFactory = spamFilterFactory;
	}

	public ISpamFilter Create(SpamFilterType spamFilterType)
	{
		return spamFilterFactory.Create(spamFilterType);
	}
}