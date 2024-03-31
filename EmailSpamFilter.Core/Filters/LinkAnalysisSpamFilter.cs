namespace EmailSpamFilter.Core.Filters;
using System.Text.RegularExpressions;
using EmailSpamFilter.Core.Models;
using Google.Apis.Safebrowsing.v4;
using Google.Apis.Safebrowsing.v4.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;

public class LinkAnalysisSpamFilter : ISpamFilter
{
	private readonly SafebrowsingService safebrowsingService;

	public LinkAnalysisSpamFilter(IConfiguration configuration)
	{
		if (string.IsNullOrWhiteSpace(configuration["googleApiKey"]))
		{
			throw new ArgumentException("Google API key is missing");
		}

		var googleApiKey = configuration["googleApiKey"];
		safebrowsingService = new SafebrowsingService(new BaseClientService.Initializer
		{
			ApiKey = googleApiKey
		});
	}

	public async Task<bool> IsSpamAsync(Email email)
	{
		var links = ExtractLinks(email.Body);

		foreach (var link in links)
		{
			var isUnsafe = await IsLinkUnsafe(link);

			if (isUnsafe)
			{
				return true;
			}
		}

		return false;
	}

	private static IEnumerable<string> ExtractLinks(string text)
	{
		const string pattern = @"^(https:|http:|www\.)\S*";
		var regex = new Regex(pattern, RegexOptions.IgnoreCase);

		return regex.Matches(text).Select(match => match.Value).ToList();
	}

	private async Task<bool> IsLinkUnsafe(string link)
	{
		var threatMatchesRequest = new GoogleSecuritySafebrowsingV4FindThreatMatchesRequest
		{
			Client = new GoogleSecuritySafebrowsingV4ClientInfo
			{
				ClientId = "EmailSpamFilter",
				ClientVersion = "1.0.0"
			},
			ThreatInfo = new GoogleSecuritySafebrowsingV4ThreatInfo
			{
				ThreatTypes = new List<string>
				{
					"MALWARE",
					"SOCIAL_ENGINEERING",
					"UNWANTED_SOFTWARE",
					"POTENTIALLY_HARMFUL_APPLICATION"
				},
				PlatformTypes = new List<string>
				{
					"WINDOWS"
				},
				ThreatEntryTypes = new List<string>
				{
					"URL"
				},
				ThreatEntries = new List<GoogleSecuritySafebrowsingV4ThreatEntry>
				{
					new GoogleSecuritySafebrowsingV4ThreatEntry { Url = link }
				}
			}
		};

		var threatMatches = await safebrowsingService.ThreatMatches.Find(threatMatchesRequest).ExecuteAsync();

		return threatMatches?.Matches?.Count > 0;
	}
}