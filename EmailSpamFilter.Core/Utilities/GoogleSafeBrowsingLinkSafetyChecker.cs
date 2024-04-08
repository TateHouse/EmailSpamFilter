namespace EmailSpamFilter.Core.Utilities;
using Google.Apis.Safebrowsing.v4;
using Google.Apis.Safebrowsing.v4.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;

/// <summary>
/// A <see cref="ILinkSafetyChecker"/> that uses Google Safe Browsing API to check if a link is safe.
/// </summary>
public class GoogleSafeBrowsingLinkSafetyChecker : ILinkSafetyChecker
{
	private readonly SafebrowsingService safebrowsingService;

	/// <summary>
	/// Instantiates a new <see cref="GoogleSafeBrowsingLinkSafetyChecker"/> instance.
	/// </summary>
	/// <param name="configuration">An instance of <see cref="IConfiguration"/> that contains the googleApiKey secrets
	/// file path.</param>
	/// <exception cref="ArgumentException">Thrown if the googleApiKey configuration value is missing.</exception>
	public GoogleSafeBrowsingLinkSafetyChecker(IConfiguration configuration)
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

	public async Task<bool> IsLinkUnsafeAsync(string link)
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