namespace EmailSpamFilter.Console.Utilities;
using EmailSpamFilter.Console.Models;

public interface IConsoleUserInterface
{
	public void DisplayAvailableFilters();
	public IEnumerable<byte> SelectFilters();
	public void DisplayFilteredEmails(IEnumerable<FilteredEmail> emails, byte indentationLevel);
}