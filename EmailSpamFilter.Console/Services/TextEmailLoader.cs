namespace EmailSpamFilter.Console.Services;
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

	public async Task<IEnumerable<string>> LoadAsync()
	{
		var emails = new List<string>();
		var files = Directory.GetFiles(path, $"*{TextEmailLoader.FileExtension}");

		foreach (var file in files)
		{
			var content = await File.ReadAllTextAsync(file);
			emails.Add(content);
		}

		return emails;
	}
}