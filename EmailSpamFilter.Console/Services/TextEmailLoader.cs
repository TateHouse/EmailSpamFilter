namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Console.Models;
using Microsoft.Extensions.Configuration;

public class TextEmailLoader : IEmailLoader
{
	private const string FileExtension = ".txt";
	private readonly string path;

	public TextEmailLoader(IConfiguration configuration)
	{
		const string key = "EmailsPath";
		var path = configuration[key];

		if (string.IsNullOrWhiteSpace(path))
		{
			throw new ArgumentException("The emails path must be provided", nameof(configuration));
		}

		path = configuration[key];

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