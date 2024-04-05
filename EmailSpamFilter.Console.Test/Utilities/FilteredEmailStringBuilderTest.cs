namespace EmailSpamFilter.Console.Test.Utilities;
using EmailSpamFilter.Console.Models;
using EmailSpamFilter.Console.Utilities;
using FluentAssertions;

[TestFixture]
public class FilteredEmailStringBuilderTest
{
	private const string FileName = "Email.txt";
	private IConsoleStringBuilder<FilteredEmail> consoleStringBuilder;

	[SetUp]
	public void SetUp()
	{
		consoleStringBuilder = new FilteredEmailStringBuilder();
	}

	[Test]
	public void GivenFilteredEmailThatIsSpam_WhenToStringWithNoIndentation_ThenReturnsFormattedString()
	{
		var filteredEmail = new FilteredEmail(FilteredEmailStringBuilderTest.FileName, true);
		var result = consoleStringBuilder.ToString(filteredEmail, 0);

		result.Should().Be($"File Name: {FilteredEmailStringBuilderTest.FileName}{Environment.NewLine}Is Spam: True");
	}

	[Test]
	public void GivenFilteredEmailThatIsNotSpam_WhenToStringWithNoIndentation_ThenReturnsFormattedString()
	{
		var filteredEmail = new FilteredEmail(FilteredEmailStringBuilderTest.FileName, false);
		var result = consoleStringBuilder.ToString(filteredEmail, 0);

		result.Should().Be($"File Name: {FilteredEmailStringBuilderTest.FileName}{Environment.NewLine}Is Spam: False");
	}

	[Test]
	public void GivenFilteredEmailThatIsSpamWithIndentation_WhenToStringWithIndentation_ThenReturnsFormattedString()
	{
		var filteredEmail = new FilteredEmail(FilteredEmailStringBuilderTest.FileName, true);
		var result = consoleStringBuilder.ToString(filteredEmail, 2);

		result.Should()
			  .Be($"\t\tFile Name: {FilteredEmailStringBuilderTest.FileName}{Environment.NewLine}\t\tIs Spam: True");
	}

	[Test]
	public void GivenFilteredEmailThatIsNotSpamWithIndentation_WhenToStringWithIndentation_ThenReturnsFormattedString()
	{
		var filteredEmail = new FilteredEmail(FilteredEmailStringBuilderTest.FileName, false);
		var result = consoleStringBuilder.ToString(filteredEmail, 2);

		result.Should()
			  .Be($"\t\tFile Name: {FilteredEmailStringBuilderTest.FileName}{Environment.NewLine}\t\tIs Spam: False");
	}
}