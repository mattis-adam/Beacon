using Beacon.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Beacon.Web.Services
{
    public class BeaconApiClient(HttpClient http)
    {
        private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter() }
        };

        public Task<StatusOverview?> GetOverviewAsync(CancellationToken ct = default) => http.GetFromJsonAsync<StatusOverview>("api/status", Json, ct);
    }
}