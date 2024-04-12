namespace EmailSpamFilter.Core.Entities;
/// <summary>
/// A class representing a filtered email with a file name and a spam status.
/// </summary>
public class FilteredEmail
{
	public string FileName { get; }
	public bool IsSpam { get; }

	/// <summary>
	/// Instantiates a new filtered email.
	/// </summary>
	/// <param name="fileName">The file name of the email, including the file extension.</param>
	/// <param name="isSpam">Whether the email is considered spam.</param>
	public FilteredEmail(string fileName, bool isSpam)
	{
		FileName = fileName;
		IsSpam = isSpam;
	}
}