namespace EmailSpamFilter.Console.Utilities;
public interface IConsoleStringBuilder<in TModel>
{
	public string ToString(TModel model, byte indentationLevel);
}