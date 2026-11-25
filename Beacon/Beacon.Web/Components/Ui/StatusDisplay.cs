using Beacon.Contracts;

namespace Beacon.Web.Components.Ui
{
    public static class StatusDisplay
    {
        public static (string Css, string Color, string Icon, string Label) For(ServiceStatus s) => s switch
        {
            ServiceStatus.Operational => new("status-operational", "var(--status-ok)", "✓", "Operational"),
            ServiceStatus.Degraded => new("status-degraded", "var(--status-degraded)", "!", "Degraded"),
            ServiceStatus.PartialOutage => new("status-partial", "var(--status-degraded)", "▲", "Partial Outage"),
            ServiceStatus.MajorOutage => new("status-major", "var(--status-outage)", "✕", "Major Outage"),
            ServiceStatus.Maintenance => new("status-maintenance", "var(--status-maintenance)", "⚙", "Maintenance"),
            _ => new("status-unknown", "var(--status-unknown)", "?", "Unknown"),
        };

        public static readonly ServiceStatus[] All =
        [
            ServiceStatus.Operational,
            ServiceStatus.Maintenance,
            ServiceStatus.Unknown,
            ServiceStatus.Degraded,
            ServiceStatus.PartialOutage,
            ServiceStatus.MajorOutage,
        ];
    }
}