namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;

/// <summary>
/// An interface for loading emails.
/// </summary>
public interface IEmailLoader
{
	/// <summary>
	/// Asynchronously loads emails.
	/// </summary>
	/// <returns>A Task representing the asynchronous operation. The Task's result is a collection of <see cref="LoadedEmail"/>.</returns>
	public Task<IEnumerable<LoadedEmail>> LoadAsync();
}