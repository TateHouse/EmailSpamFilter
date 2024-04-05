namespace EmailSpamFilter.Console.Utilities;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Core.Filters;
using Console=System.Console;

public class ConsoleUserInterface
{
	private const int PrimarySeparatorLength = 50;
	private const int SecondarySeparatorLength = 50;
	private readonly string primarySeparator = new string('=', ConsoleUserInterface.PrimarySeparatorLength);
	private readonly string secondarySeparator = new string('-', ConsoleUserInterface.SecondarySeparatorLength);
	private readonly IReadOnlyDictionary<byte, SpamFilterType> availableSpamFilters;
	private readonly IConsoleStringBuilder<FilteredEmail> filteredEmailStringBuilder;

	public ConsoleUserInterface(IReadOnlyDictionary<byte, SpamFilterType> availableSpamFilters,
								IConsoleStringBuilder<FilteredEmail> filteredEmailStringBuilder)
	{
		this.availableSpamFilters = availableSpamFilters;
		this.filteredEmailStringBuilder = filteredEmailStringBuilder;
	}

	public void DisplayAvailableFilters()
	{
		PrintPrimarySeparator();
		Console.WriteLine("Available Spam Filters");
		PrintPrimarySeparator();

		foreach (var spamFilterType in availableSpamFilters)
		{
			Console.WriteLine($"{spamFilterType.Key}: {spamFilterType.Value}");
		}

		PrintPrimarySeparator();
	}

	public IEnumerable<byte> SelectFilters()
	{
		const string exitOption = "-1";

		while (true)
		{
			try
			{
				PrintPrimarySeparator();
				Console.WriteLine($"Select the spam filters you would like to use or {exitOption} to exit. (comma separated):");
				Console.WriteLine("\tExample: 1, 2");

				var input = Console.ReadLine();

				if (input == null || input.Trim() == exitOption)
				{
					Environment.Exit(0);
				}

				var selectedFilters = input.Trim().Split(',').Select(byte.Parse).ToList();

				if (selectedFilters.Count == 0)
				{
					throw new ArgumentNullException();
				}

				if (selectedFilters.Exists(filter => !availableSpamFilters.ContainsKey(filter)))
				{
					throw new FormatException();
				}

				PrintPrimarySeparator();

				return selectedFilters;
			}
			catch (Exception exception) when (exception is ArgumentNullException ||
											  exception is FormatException ||
											  exception is OverflowException)
			{
				Console.WriteLine("Invalid input. Please try again.");
			}
		}
	}

	public void DisplayFilteredEmails(IEnumerable<FilteredEmail> filteredEmails, byte indentationLevel)
	{
		PrintPrimarySeparator();

		foreach (var filteredEmail in filteredEmails)
		{
			PrintSecondarySeparator();
			var formatted = filteredEmailStringBuilder.ToString(filteredEmail, indentationLevel);
			Console.WriteLine(formatted);
			PrintSecondarySeparator();
		}

		PrintPrimarySeparator();
	}

	private void PrintPrimarySeparator()
	{
		Console.WriteLine(primarySeparator);
	}

	private void PrintSecondarySeparator()
	{
		Console.WriteLine(secondarySeparator);
	}
}