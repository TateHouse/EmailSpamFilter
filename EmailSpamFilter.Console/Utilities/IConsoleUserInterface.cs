namespace EmailSpamFilter.Console.Utilities;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Filters;

/// <summary>
/// An interface for user interfaces.
/// </summary>
public interface IConsoleUserInterface
{
	/// <summary>
	/// Displays the available filters to the console.
	/// </summary>
	public void DisplayAvailableFilters();

	/// <summary>
	/// Gets the selected <see cref="ISpamFilter"/> from user input through the console.
	/// </summary>
	/// <returns>A collection of <see cref="ISpamFilter"/> ids to use when filtering emails.</returns>
	public IEnumerable<byte> SelectFilters();

	/// <summary>
	/// Displays the <see cref="FilteredEmail"/> to the console.
	/// </summary>
	/// <param name="emails">A collection of <see cref="FilteredEmail"/> to display.</param>
	/// <param name="indentationLevel">The indentation level for the formatted string representation of the
	/// <see cref="FilteredEmail"/>.</param>
	public void DisplayFilteredEmails(IEnumerable<FilteredEmail> emails, byte indentationLevel);
}