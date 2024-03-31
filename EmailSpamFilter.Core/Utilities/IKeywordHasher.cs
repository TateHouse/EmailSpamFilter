namespace EmailSpamFilter.Core.Utilities;
public interface IKeywordHasher
{
	public string HashKeyword(string keyword);
}