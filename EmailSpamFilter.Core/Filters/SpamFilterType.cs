namespace EmailSpamFilter.Core.Filters;
/// <summary>
/// An enum that represents the type of <see cref="ISpamFilter"/>.
/// </summary>
public enum SpamFilterType
{
	KeywordSignature = 1,
	LinkAnalysis = 2,
	UnsubscribeLink = 3
}