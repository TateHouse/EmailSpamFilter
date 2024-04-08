namespace EmailSpamFilter.Console.Utilities;
/// <summary>
/// An interface for formatting strings for console output.
/// </summary>
/// <typeparam name="TModel">The type of model to format.</typeparam>
public interface IConsoleStringBuilder<in TModel>
{
	/// <summary>
	/// Formats a model as a string.
	/// </summary>
	/// <param name="model">An instance of <see cref="TModel"/> to format.</param>
	/// <param name="indentationLevel">The indentation level for the formatted string.</param>
	/// <returns>A string representation of the <see cref="TModel"/>, formatted for console output.</returns>
	public string ToString(TModel model, byte indentationLevel);
}