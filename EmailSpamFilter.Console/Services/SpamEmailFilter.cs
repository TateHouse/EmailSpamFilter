namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Filters;

public class SpamEmailFilter : ISpamEmailFilter
{
	private readonly IEnumerable<ISpamFilter> spamFilters;
	private readonly Email email;

	public SpamEmailFilter(IEnumerable<ISpamFilter> spamFilters, Email email)
	{
		this.spamFilters = spamFilters.ToList();

		if (!this.spamFilters.Any())
		{
			throw new ArgumentException("At least one spam filter is required.", nameof(spamFilters));
		}

		this.email = email;
	}

	public async Task<FilteredEmail> FilterAsync()
	{
		var isSpam = false;

		foreach (var spamFilter in spamFilters)
		{
			isSpam = await spamFilter.IsSpamAsync(email);

			if (!isSpam)
			{
				break;
			}
		}

		return new FilteredEmail(email, isSpam);
	}
}