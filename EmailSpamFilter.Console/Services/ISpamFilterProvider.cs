namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Filters;
using System.Collections.ObjectModel;

/// <summary>
/// An interface for providing <see cref="ISpamFilter"/>.
/// </summary>
public interface ISpamFilterProvider
{
	public ReadOnlyDictionary<byte, SpamFilterType> AvailableSpamFilterTypes { get; }

	/// <summary>
	/// Instantiates a <see cref="ISpamFilter"/>.
	/// </summary>
	/// <param name="spamFilterType">The type of <see cref="ISpamFilter"/> to instantiate.</param>
	/// <returns>An instance of <see cref="ISpamFilter"/>.</returns>
	public ISpamFilter Create(SpamFilterType spamFilterType);
}