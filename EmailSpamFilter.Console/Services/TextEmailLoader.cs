namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;

public class TextEmailLoader : IEmailLoader
{
	private const string FileExtension = ".txt";
	private readonly string path;

	public TextEmailLoader(string path)
	{
		if (string.IsNullOrWhiteSpace(path))
		{
			throw new ArgumentException("The path cannot be null or whitespace.", nameof(path));
		}

		if (!Directory.Exists(path))
		{
			throw new DirectoryNotFoundException("The provided path does not exist.");
		}

		this.path = path;
	}

	public async Task<IEnumerable<LoadedEmail>> LoadAsync()
	{
		var emails = new List<LoadedEmail>();
		var files = Directory.GetFiles(path, $"*{TextEmailLoader.FileExtension}");

		foreach (var file in files)
		{
			var name = Path.GetFileName(file);
			var content = await File.ReadAllTextAsync(file);
			var loadedEmail = new LoadedEmail(name, content);
			emails.Add(loadedEmail);
		}

		return emails;
	}
}