namespace EmailSpamFilter.Core.Utilities;
/// <summary>
/// An interface for checking if a link is safe.
/// </summary>
public interface ILinkSafetyChecker
{
	/// <summary>
	/// Asynchronously checks if a link is safe.
	/// </summary>
	/// <param name="link">A link to check.</param>
	/// <returns>A Task representing the asynchronous operation. The Task's result is a boolean indicating whether the
	/// link is unsafe.</returns>
	public Task<bool> IsLinkUnsafeAsync(string link);
}