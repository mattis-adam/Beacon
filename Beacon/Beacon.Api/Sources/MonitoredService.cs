namespace Beacon.Api.Sources
{
    public sealed record MonitoredService(
        string Id,
        string Name,
        string Url
    );
}