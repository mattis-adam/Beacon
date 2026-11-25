using Beacon.Contracts;
using System.Text.Json.Serialization;

namespace Beacon.Api.Sources
{
    public sealed class StatuspageClient(HttpClient http)
    {
        public async Task<SourceSnapshot> FetchAsync(MonitoredService service, CancellationToken ct)
        {
            var url = $"{service.Url.TrimEnd('/')}/api/v2/summary.json";

            var summary = await http.GetFromJsonAsync<SummaryDto>(url, ct) ?? throw new InvalidOperationException($"Réponse vide de {url}");

            return new SourceSnapshot(
                Id: service.Id,
                Name: service.Name,
                Url: service.Url,
                Status: MapIndicator(summary.Status.Indicator),
                Description: summary.Status.Description,
                SourceUpdatedAt: summary.Page.UpdatedAt,
                LastCheckedAt: DateTimeOffset.UtcNow);
        }
        public static ServiceStatus MapIndicator(string? indicator) => indicator switch
        {
            "none" => ServiceStatus.Operational,
            "minor" => ServiceStatus.Degraded,
            "major" => ServiceStatus.PartialOutage,
            "critical" => ServiceStatus.MajorOutage,
            "maintenance" => ServiceStatus.Maintenance,
            _ => ServiceStatus.Unknown
        };

        private sealed record SummaryDto(PageDto Page, StatusDto Status);

        private sealed record PageDto([property: JsonPropertyName("updated_at")] DateTimeOffset? UpdatedAt);

        private sealed record StatusDto(string? Indicator, string? Description);

    }
}