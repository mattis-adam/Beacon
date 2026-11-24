using System.Text.Json;

namespace Beacon_V1
{
    public class Client
    {
        private static readonly HttpClient _http = CreateClient();
        private static HttpClient CreateClient()
        {
            var http = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
            http.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
            return http;
        }

        public static async Task<JsonDocument> GetJsonAsync(string url)
        {
            await using var stream = await _http.GetStreamAsync(url);
            return await JsonDocument.ParseAsync(stream);
        }
    }
}
