﻿namespace EmailSpamFilter.Console.Test.Services;
using EmailSpamFilter.Console.Services;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Filters;
using FluentAssertions;
using Moq;

[TestFixture]
public class SpamEmailFilterTest
{
	private Mock<List<ISpamFilter>> mockSpamFilters;
	private Email email;
	private ISpamEmailFilter spamEmailFilter;

	[SetUp]
	public void SetUp()
	{
		email = new Email("Subject", "Body");
		mockSpamFilters = new Mock<List<ISpamFilter>>();
	}

	[Test]
	public void GivenNoSpamFilters_WhenInstantiate_ThenThrowsArgumentException()
	{
		var action = () => new SpamEmailFilter(mockSpamFilters.Object, email);
		action.Should().Throw<ArgumentException>();
	}

	[Test]
	public async Task GivenSpamFilterThatReturnsFalse_WhenFilterAsync_ThenReturnsFilteredEmail()
	{
		var spamFilterResults = new List<bool> { false };
		CreateMockSpamFilters(spamFilterResults);
		spamEmailFilter = new SpamEmailFilter(mockSpamFilters.Object, email);

		var result = await spamEmailFilter.FilterAsync();

		result.IsSpam.Should().BeFalse();
	}

	[Test]
	public async Task GivenSpamFilterThatReturnsTrue_WhenFilterAsync_ThenReturnsFilteredEmail()
	{
		var spamFilterResults = new List<bool> { true };
		CreateMockSpamFilters(spamFilterResults);
		spamEmailFilter = new SpamEmailFilter(mockSpamFilters.Object, email);

		var result = await spamEmailFilter.FilterAsync();

		result.IsSpam.Should().BeTrue();
	}

	[Test]
	public async Task GivenSpamFiltersThatAllReturnTrue_WhenFilterAsync_ThenReturnsFilteredEmail()
	{
		var spamFilterResults = new List<bool> { true, true, true };
		CreateMockSpamFilters(spamFilterResults);
		spamEmailFilter = new SpamEmailFilter(mockSpamFilters.Object, email);

		var result = await spamEmailFilter.FilterAsync();

		result.IsSpam.Should().BeTrue();
	}

	[Test]
	public async Task GivenSpamFiltersThatReturnFalseAndTrue_WhenFilterAsync_ThenReturnsFilteredEmail()
	{
		var spamFilterResults = new List<bool> { true, false, true };
		CreateMockSpamFilters(spamFilterResults);
		spamEmailFilter = new SpamEmailFilter(mockSpamFilters.Object, email);

		var result = await spamEmailFilter.FilterAsync();

		result.IsSpam.Should().BeFalse();
	}

	private void CreateMockSpamFilters(IEnumerable<bool> spamFilterResults)
	{
		foreach (var result in spamFilterResults)
		{
			var mockSpamFilter = new Mock<ISpamFilter>();
			mockSpamFilter.Setup(mock => mock.IsSpamAsync(email)).ReturnsAsync(result);
			mockSpamFilters.Object.Add(mockSpamFilter.Object);
		}
	}
}