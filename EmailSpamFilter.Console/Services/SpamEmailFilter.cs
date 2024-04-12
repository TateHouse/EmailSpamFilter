namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Filters;
using EmailSpamFilter.Core.Services;

/// <summary>
/// A class for filtering spam emails.
/// </summary>
public class SpamEmailFilter : ISpamEmailFilter
{
	private readonly IEnumerable<ISpamFilter> spamFilters;
	private readonly ParsedEmail parsedEmail;

	/// <summary>
	/// Instantiates a <see cref="ISpamFilter"/>.
	/// </summary>
	/// <param name="spamFilters">A collection of <see cref="ISpamFilter"/> to use in filtering the email.</param>
	/// <param name="parsedEmail">A <see cref="ParsedEmail"/> to be filtered.</param>
	/// <exception cref="ArgumentException">Thrown if no <see cref="ISpamFilter"/> are provided.</exception>
	public SpamEmailFilter(IEnumerable<ISpamFilter> spamFilters, ParsedEmail parsedEmail)
	{
		this.spamFilters = spamFilters.ToList();

		if (!this.spamFilters.Any())
		{
			throw new ArgumentException("At least one spam filter is required.", nameof(spamFilters));
		}

		this.parsedEmail = parsedEmail;
	}

	public async Task<FilteredEmail> FilterAsync()
	{
		var isSpam = false;

		foreach (var spamFilter in spamFilters)
		{
			var email = new Email(parsedEmail.Subject, parsedEmail.Body);
			isSpam = await spamFilter.IsSpamAsync(email);

			if (!isSpam)
			{
				break;
			}
		}

		return new FilteredEmail(parsedEmail.FileName, isSpam);
	}
}