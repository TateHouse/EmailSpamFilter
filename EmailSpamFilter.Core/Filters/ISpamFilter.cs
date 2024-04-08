namespace EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Entities;

/// <summary>
/// An interface for spam filters.
/// </summary>
public interface ISpamFilter
{
	/// <summary>
	/// Checks if an <see cref="Email"/> is considered spam.
	/// </summary>
	/// <param name="email">The <see cref="Email"/> is check.</param>
	/// <returns>True if the <see cref="Email"/> is considered as spam. Otherwise, false.</returns>
	public Task<bool> IsSpamAsync(Email email);
}