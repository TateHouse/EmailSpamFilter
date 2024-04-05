namespace EmailSpamFilter.Console.Test.Services;
using EmailSpamFilter.Console.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

[TestFixture]
public class TextEmailLoaderTest
{
	private Mock<IConfiguration> mockConfiguration;
	private IEmailLoader emailLoader;

	[SetUp]
	public void SetUp()
	{
		mockConfiguration = new Mock<IConfiguration>();
		mockConfiguration.Setup(mock => mock["EmailsPath"])
						 .Returns("EmailSpamFilter.Console.Test/Services/TextEmailLoaderTest");
		Directory.CreateDirectory(mockConfiguration.Object["EmailsPath"]!);
		emailLoader = new TextEmailLoader(mockConfiguration.Object);
	}

	[TearDown]
	public void TearDown()
	{
		Directory.Delete(mockConfiguration.Object["EmailsPath"]!, true);
	}

	[Test]
	public void GivenEmptyPath_WhenInstantiate_ThenThrowsArgumentException()
	{
		mockConfiguration.Reset();
		mockConfiguration.Setup(mock => mock["EmailsPath"]).Returns(string.Empty);
		var action = () => new TextEmailLoader(mockConfiguration.Object);

		action.Should().Throw<ArgumentException>();

		mockConfiguration.Reset();
		mockConfiguration.Setup(mock => mock["EmailsPath"])
						 .Returns("EmailSpamFilter.Console.Test/Services/TextEmailLoaderTest");
	}

	[Test]
	public void GivenNonExistentPath_WhenInstantiate_ThenThrowsDirectoryNotFoundException()
	{
		mockConfiguration.Reset();
		mockConfiguration.Setup(mock => mock["EmailsPath"]).Returns("NonExistentPath/Emails");
		var action = () => new TextEmailLoader(mockConfiguration.Object);

		action.Should().Throw<DirectoryNotFoundException>();

		mockConfiguration.Reset();
		mockConfiguration.Setup(mock => mock["EmailsPath"])
						 .Returns("EmailSpamFilter.Console.Test/Services/TextEmailLoaderTest");
	}

	[Test]
	public async Task GivenPathWithSingleFile_WhenLoadAsync_ThenReturnsFileContent()
	{
		CreateEmailTextFiles(1);

		var emails = (await emailLoader.LoadAsync()).ToList();
		emails.Should().HaveCount(1);
		emails[0].Source.Should().Be("This is the subject line for email 0\nThis is the body for email 0.");
	}

	[Test]
	public async Task GivenPathWithMultipleFiles_WhenLoadAsync_ThenReturnsFileContents()
	{
		CreateEmailTextFiles(8);

		var emails = (await emailLoader.LoadAsync()).ToList();
		emails.Should().HaveCount(8);
		emails[0].Source.Should().Be("This is the subject line for email 0\nThis is the body for email 0.");
		emails[7].Source.Should().Be("This is the subject line for email 7\nThis is the body for email 7.");
	}

	private void CreateEmailTextFiles(byte emailCount)
	{
		for (var index = 0; index < emailCount; ++index)
		{
			var content = $"This is the subject line for email {index}\nThis is the body for email {index}.";
			var nameWithExtension = $"Email_{index}.txt";
			var path = mockConfiguration.Object["EmailsPath"]!;
			File.WriteAllText(Path.Combine(path, nameWithExtension), content);
		}
	}
}