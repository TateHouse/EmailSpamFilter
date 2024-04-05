namespace EmailSpamFilter.Console.Services;
public interface IEmailLoader
{
	public Task<IEnumerable<string>> LoadAsync();
}