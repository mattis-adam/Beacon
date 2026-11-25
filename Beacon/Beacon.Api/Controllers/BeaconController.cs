using Beacon.Api.Sources;
using Beacon.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Beacon.Api.Controllers
{

    [ApiController]
    [Route("api/status")]
    public class BeaconController(
        StatuspageClient client, 
        IReadOnlyList<MonitoredService> services) : ControllerBase
    {
        private static readonly ServiceStatus[] Severity =
        [
            ServiceStatus.Operational,
                ServiceStatus.Maintenance,
                ServiceStatus.Unknown,
                ServiceStatus.Degraded,
                ServiceStatus.PartialOutage,
                ServiceStatus.MajorOutage
        ];

        [HttpGet]
        public async Task<StatusOverview> Get(CancellationToken ct)
        {
            var snapshots = await Task.WhenAll(services.Select(s => FetchOrUnknown(s, ct)));
            return new StatusOverview(Worst(snapshots), snapshots);
        }
        private async Task<SourceSnapshot> FetchOrUnknown(MonitoredService s, CancellationToken ct)
        {
            try
            {
                return await client.FetchAsync(s, ct);
            }
            catch (Exception) when (!ct.IsCancellationRequested)
            {
                return new SourceSnapshot(s.Id, s.Name, s.Url,
                    ServiceStatus.Unknown, null, null, DateTimeOffset.UtcNow);
            }
        }

        private static ServiceStatus Worst(IEnumerable<SourceSnapshot> snapshots) => 
            snapshots.MaxBy(x => Array.IndexOf(Severity, x.Status))?.Status ?? ServiceStatus.Unknown;
    }
}