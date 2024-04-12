namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Services;

/// <summary>
/// An interface for creating <see cref="ISpamFilter"/> instances.
/// </summary>
public class SpamEmailFilterFactory : ISpamEmailFilterFactory
{
	/// <summary>
	/// Instantiates a <see cref="ISpamEmailFilter"/>.
	/// </summary>
	/// <param name="spamFilters">A collection of <see cref="ISpamFilter"/> to use in filtering the email.</param>
	/// <param name="email">A <see cref="ParsedEmail"/> to be filtered.</param>
	/// <returns>An instance of <see cref="ISpamEmailFilter"/>.</returns>
	public ISpamEmailFilter Create(IEnumerable<ISpamFilter> spamFilters, ParsedEmail email)
	{
		return new SpamEmailFilter(spamFilters, email);
	}
}