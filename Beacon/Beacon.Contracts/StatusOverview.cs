namespace Beacon.Contracts
{
    public sealed record StatusOverview(ServiceStatus GlobalStatus, IReadOnlyList<SourceSnapshot> Sources);
}