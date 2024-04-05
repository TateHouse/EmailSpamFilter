namespace EmailSpamFilter.Console;
public static class Program
{
	public async static Task Main(string[] args)
	{
		var application = new Application("AppSettings.json", "Secrets.json");
		await application.Run();
	}
}