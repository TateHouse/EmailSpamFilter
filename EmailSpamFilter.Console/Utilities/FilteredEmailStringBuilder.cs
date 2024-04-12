namespace EmailSpamFilter.Console.Utilities;
using EmailSpamFilter.Core.Entities;
using System.Text;

/// <summary>
/// A class that builds a string representation of a <see cref="FilteredEmail"/> model.
/// </summary>
public class FilteredEmailStringBuilder : IConsoleStringBuilder<FilteredEmail>
{
	public string ToString(FilteredEmail model, byte indentationLevel)
	{
		var stringBuilder = new StringBuilder();
		var indentation = new string('\t', indentationLevel);

		stringBuilder.AppendLine($"{indentation}File Name: {model.FileName}");
		stringBuilder.Append($"{indentation}Is Spam: {model.IsSpam}");

		return stringBuilder.ToString();
	}
}