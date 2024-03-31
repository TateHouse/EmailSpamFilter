namespace EmailSpamFilter.Core.Utilities;
public interface ILinkSafetyChecker
{
	public Task<bool> IsLinkUnsafeAsync(string link);
}