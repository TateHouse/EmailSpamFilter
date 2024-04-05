namespace EmailSpamFilter.Console.Models;
public class FilteredEmail
{
	public string FileName { get; }
	public bool IsSpam { get; }

	public FilteredEmail(string fileName, bool isSpam)
	{
		FileName = fileName;
		IsSpam = isSpam;
	}
}