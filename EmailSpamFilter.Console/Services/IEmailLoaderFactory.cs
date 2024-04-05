namespace EmailSpamFilter.Console.Services;
public interface IEmailLoaderFactory
{
	public IEmailLoader Create();
}