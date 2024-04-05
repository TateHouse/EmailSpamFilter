namespace EmailSpamFilter.Console.Models;
using EmailSpamFilter.Core.Entities;

public class FilteredEmail
{
	public Email Email { get; }
	public bool IsSpam { get; }

	public FilteredEmail(Email email, bool isSpam)
	{
		Email = email ?? throw new ArgumentNullException(nameof(email));
		IsSpam = isSpam;
	}
}