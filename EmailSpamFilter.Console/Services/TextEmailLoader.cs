namespace EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Services;
using Microsoft.Extensions.Configuration;

/// <summary>
/// A class for loading emails stored in text files from a directory stored in the configuration.
/// </summary>
/// <remarks>
/// The email files must have a .txt extension and be in the format where the first line is the subject and the rest is
/// the body of the email.
/// </remarks>
public class TextEmailLoader : IEmailLoader
{
	private const string FileExtension = ".txt";
	private readonly string path;

	/// <summary>
	/// Instantiates a new <see cref="TextEmailLoader"/> instance.
	/// </summary>
	/// <param name="configuration">An instance of <see cref="IConfiguration"/> that contains the EmailsPath.</param>
	/// <exception cref="ArgumentException">Thrown if the EmailsPath configuration value is missing.</exception>
	/// <exception cref="DirectoryNotFoundException">Thrown if the EmailsPath was not found.</exception>
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