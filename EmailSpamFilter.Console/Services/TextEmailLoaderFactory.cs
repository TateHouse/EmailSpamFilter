namespace EmailSpamFilter.Console.Services;
using Microsoft.Extensions.Configuration;

/// <summary>
/// A factory for creating instances of IEmailLoader.
/// </summary>
public class TextEmailLoaderFactory : IEmailLoaderFactory
{
	private readonly IConfiguration configuration;

	/// <summary>
	/// Instantiates a new <see cref="TextEmailLoaderFactory"/> instance.
	/// </summary>
	/// <param name="configuration">An instance of <see cref="IConfiguration"/> that contains the EmailsPath.</param>
	public TextEmailLoaderFactory(IConfiguration configuration)
	{
		this.configuration = configuration;
	}

	public IEmailLoader Create()
	{
		return new TextEmailLoader(configuration);
	}
}