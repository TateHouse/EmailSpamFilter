namespace EmailSpamFilter.Console.Utilities;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Filters;
using Console=System.Console;

/// <summary>
/// A class for allowing user interaction through the console.
/// </summary>
public class ConsoleUserInterface : IConsoleUserInterface
{
	private const int PrimarySeparatorLength = 50;
	private const int SecondarySeparatorLength = 50;
	private readonly string primarySeparator = new string('=', ConsoleUserInterface.PrimarySeparatorLength);
	private readonly string secondarySeparator = new string('-', ConsoleUserInterface.SecondarySeparatorLength);

	private readonly IReadOnlyDictionary<byte, SpamFilterType> spamFilterTypes;
	private readonly IConsoleStringBuilder<FilteredEmail> emailStringBuilder;

	/// <summary>
	/// Instantiates a <see cref="ConsoleUserInterface"/>.
	/// </summary>
	/// <param name="spamFilterProvider">An instance of <see cref="ISpamFilterProvider"/> used for providing
	/// <see cref="ISpamFilter"/>.</param>
	/// <param name="emailStringBuilder">An instance of <see cref="IConsoleStringBuilder{FilteredEmail}"/> used for formatting
	/// <see cref="FilteredEmail"/> into strings.</param>
	public ConsoleUserInterface(ISpamFilterProvider spamFilterProvider,
								IConsoleStringBuilder<FilteredEmail> emailStringBuilder)
	{
		spamFilterTypes = spamFilterProvider.AvailableSpamFilterTypes;
		this.emailStringBuilder = emailStringBuilder;
	}

	public void DisplayAvailableFilters()
	{
		PrintPrimarySeparator();
		Console.WriteLine("Available Spam Filters");
		PrintPrimarySeparator();

		foreach (var spamFilterType in spamFilterTypes)
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

				if (selectedFilters.Exists(filter => !spamFilterTypes.ContainsKey(filter)))
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

	public void DisplayFilteredEmails(IEnumerable<FilteredEmail> emails, byte indentationLevel)
	{
		PrintPrimarySeparator();

		var sortedEmails = emails.OrderBy(email => email.FileName).ToList();

		foreach (var email in sortedEmails)
		{
			PrintSecondarySeparator();
			var formatted = emailStringBuilder.ToString(email, indentationLevel);
			Console.WriteLine(formatted);
			PrintSecondarySeparator();
		}

		PrintPrimarySeparator();

		var indentation = new string('\t', indentationLevel);
		Console.WriteLine($"{indentation}Filtered Emails Count: {sortedEmails.Count}");

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