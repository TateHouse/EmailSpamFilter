namespace EmailSpamFilter.Console.Models;
public class LoadedEmail
{
	public string FileName { get; }
	public string Source { get; }

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