namespace EmailSpamFilter.Console.Models;
/// <summary>
/// A class representing a loaded email with a file name and source.
/// </summary>
public class LoadedEmail
{
	public string FileName { get; }
	public string Source { get; }

	/// <summary>
	/// Instantiates a new loaded email.
	/// </summary>
	/// <param name="fileName">The file name of the email, including the file name extension.</param>
	/// <param name="source">The email file's loaded content.</param>
	/// <exception cref="ArgumentException">Thrown if the email file has no content.</exception>
	public LoadedEmail(string fileName, string source)
	{
		if (string.IsNullOrWhiteSpace(source))
		{
			throw new ArgumentException("The source cannot be null or whitespace.", nameof(source));
		}

		FileName = fileName;
		Source = source;
	}
}