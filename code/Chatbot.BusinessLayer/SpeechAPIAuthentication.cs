using System;
using System.Net.Http;
using System.Threading.Tasks;
using log4net;

public class SpeechAPIAuthentication
{
    private static readonly ILog log = LogManager.GetLogger(typeof(SpeechAPIAuthentication));
    public static readonly string FetchTokenUri = "https://api.cognitive.microsoft.com/sts/v1.0";
    private string subscriptionKey;
    private string token;

    public SpeechAPIAuthentication(string subscriptionKey)
    {
        this.subscriptionKey = subscriptionKey;
        this.token = FetchTokenAsync(FetchTokenUri, subscriptionKey).Result;
    }

    public string GetAccessToken()
    {
        log.Debug("subscriptionKey: " + subscriptionKey +
            "\ntoken: " + token);
        return this.token;
    }

    private async Task<string> FetchTokenAsync(string fetchUri, string subscriptionKey)
    {
        log.Debug("FetchTokenAsync");
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            UriBuilder uriBuilder = new UriBuilder(fetchUri);
            uriBuilder.Path += "/issueToken";

            var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null).ConfigureAwait(false);
            log.Debug("Token Uri: " + uriBuilder.Uri.AbsoluteUri);
            return await result.Content.ReadAsStringAsync();
        }
    }
}