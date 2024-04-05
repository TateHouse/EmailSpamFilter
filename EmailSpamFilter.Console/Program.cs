namespace EmailSpamFilter.Console;
using Microsoft.Extensions.Configuration;

public static class Program
{
	public static void Main(string[] args)
	{
		var configurationBuilder = new ConfigurationBuilder();
		configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
		configurationBuilder.AddJsonFile("AppSettings.json", false, true);
		var configuration = configurationBuilder.Build();
	}
}