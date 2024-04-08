namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;

/// <summary>
/// An interface for filtering spam emails.
/// </summary>
public interface ISpamEmailFilter
{
	/// <summary>
	/// Asynchronously filters spam emails.
	/// </summary>
	/// <returns>A Task representing the asynchronous operation. The Task's result is a <see cref="FilteredEmail"/>.</returns>
	public Task<FilteredEmail> FilterAsync();
}