namespace EmailSpamFilter.Core.Services;
using EmailSpamFilter.Core.Entities;

/// <summary>
/// An interface for creating <see cref="IEmailParser"/> instances.
/// </summary>
public interface IEmailParserFactory
{
	/// <summary>
	/// Instantiates an <see cref="IEmailParser"/>
	/// </summary>
	/// <param name="email">The <see cref="LoadedEmail"/> to parse.</param>
	/// <returns>An instance of <see cref="IEmailParser"/>.</returns>
	public IEmailParser Create(LoadedEmail email);
}