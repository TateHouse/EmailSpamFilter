﻿namespace EmailSpamFilter.Console.Utilities;
using EmailSpamFilter.Console.Models;
using System.Text;

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