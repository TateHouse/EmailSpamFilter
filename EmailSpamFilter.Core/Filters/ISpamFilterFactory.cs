namespace EmailSpamFilter.Core.Filters;

/// <summary>
/// An interface for creating <see cref="ISpamFilter"/>.
/// </summary>
public interface ISpamFilterFactory
{
	/// <summary>
	/// Instantiates a new <see cref="ISpamFilter"/> instance of the specified type.
	/// </summary>
	/// <param name="spamFilterType">The type of <see cref="ISpamFilter"/> to instantiate.</param>
	/// <returns>An instance of <see cref="ISpamFilter"/> instance of the specified type.</returns>
	public ISpamFilter Create(SpamFilterType spamFilterType);
}