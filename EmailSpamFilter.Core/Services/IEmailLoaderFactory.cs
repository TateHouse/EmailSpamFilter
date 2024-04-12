namespace EmailSpamFilter.Core.Services;

/// <summary>
/// An interface for creating <see cref="IEmailLoader"/> instances.
/// </summary>
public interface IEmailLoaderFactory
{
	/// <summary>
	/// Instantiates an email loader.
	/// </summary>
	/// <returns>An instance of <see cref="IEmailLoader"/>.</returns>
	public IEmailLoader Create();
}