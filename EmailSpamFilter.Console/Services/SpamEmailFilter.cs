namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Filters;

public class SpamEmailFilter : ISpamEmailFilter
{
	private readonly IEnumerable<ISpamFilter> spamFilters;
	private readonly ParsedEmail parsedEmail;

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