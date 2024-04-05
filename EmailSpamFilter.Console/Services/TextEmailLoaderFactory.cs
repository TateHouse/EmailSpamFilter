namespace EmailSpamFilter.Console.Services;
using Microsoft.Extensions.Configuration;

public class TextEmailLoaderFactory : IEmailLoaderFactory
{
	private readonly IConfiguration configuration;

	public TextEmailLoaderFactory(IConfiguration configuration)
	{
		this.configuration = configuration;
	}

	public IEmailLoader Create()
	{
		return new TextEmailLoader(configuration);
	}
}