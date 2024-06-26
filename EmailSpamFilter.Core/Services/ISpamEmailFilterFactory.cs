﻿namespace EmailSpamFilter.Core.Services;
using EmailSpamFilter.Core.Entities;
using EmailSpamFilter.Core.Filters;

/// <summary>
/// An interface for creating <see cref="ISpamEmailFilter"/>.
/// </summary>
public interface ISpamEmailFilterFactory
{
	/// <summary>
	/// Instantiates a <see cref="ISpamEmailFilter"/>.
	/// </summary>
	/// <param name="spamFilters">A collection of <see cref="ISpamFilter"/> to use in filtering the email.</param>
	/// <param name="email">A <see cref="ParsedEmail"/> to be filtered.</param>
	/// <returns>An instance of <see cref="ISpamEmailFilter"/>.</returns>
	public ISpamEmailFilter Create(IEnumerable<ISpamFilter> spamFilters, ParsedEmail email);
}