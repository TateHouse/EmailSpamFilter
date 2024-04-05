namespace EmailSpamFilter.Console.Test.Services;
using EmailSpamFilter.Console.Services;
using FluentAssertions;

[TestFixture]
public class TextEmailLoaderTest
{
	private string path;
	private IEmailLoader emailLoader;

	[SetUp]
	public void SetUp()
	{
		path = Path.Combine(Path.GetTempPath(), "EmailSpamFilter.Console.Test/Services/TextEmailLoaderTest");
		Directory.CreateDirectory(path);
		emailLoader = new TextEmailLoader(path);
	}

	[TearDown]
	public void TearDown()
	{
		Directory.Delete(path, true);
	}

	[Test]
	public void GivenEmptyPath_WhenInstantiate_ThenThrowsArgumentException()
	{
		var action = () => new TextEmailLoader(string.Empty);
		action.Should().Throw<ArgumentException>();
	}

	[Test]
	public void GivenNonExistentPath_WhenInstantiate_ThenThrowsDirectoryNotFoundException()
	{
		var action = () => new TextEmailLoader("NonExistentPath/Emails");
		action.Should().Throw<DirectoryNotFoundException>();
	}

	[Test]
	public async Task GivenPathWithSingleFile_WhenLoadAsync_ThenReturnsFileContent()
	{
		CreateEmailTextFiles(1);

		var emails = (await emailLoader.LoadAsync()).ToList();
		emails.Should().HaveCount(1);
		emails[0].Should().Be("This is the subject line for email 0\nThis is the body for email 0.");
	}

	[Test]
	public async Task GivenPathWithMultipleFiles_WhenLoadAsync_ThenReturnsFileContents()
	{
		CreateEmailTextFiles(8);

		var emails = (await emailLoader.LoadAsync()).ToList();
		emails.Should().HaveCount(8);
		emails[0].Should().Be("This is the subject line for email 0\nThis is the body for email 0.");
		emails[7].Should().Be("This is the subject line for email 7\nThis is the body for email 7.");
	}

	private void CreateEmailTextFiles(byte emailCount)
	{
		for (var index = 0; index < emailCount; index++)
		{
			var content = $"This is the subject line for email {index}\nThis is the body for email {index}.";
			var nameWithExtension = $"Email_{index}.txt";
			File.WriteAllText(Path.Combine(path, nameWithExtension), content);
		}
	}
}