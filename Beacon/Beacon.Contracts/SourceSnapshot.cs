namespace Beacon.Contracts
{
    public sealed record SourceSnapshot(
        string Id,
        string Name,
        string Url,
        ServiceStatus Status,
        string? Description,
        DateTimeOffset? SourceUpdatedAt,
        DateTimeOffset? LastCheckedAt);
}