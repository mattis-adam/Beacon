using Beacon.Contracts;

namespace Beacon.Web.Components.Ui
{
    public static class StatusDisplay
    {
        public static (string Css, string Color, string Icon, string Label) For(ServiceStatus s) => s switch
        {
            ServiceStatus.Operational => new("status-operational", "var(--status-ok)", "✓", "Opérationnel"),
            ServiceStatus.Degraded => new("status-degraded", "var(--status-degraded)", "!", "Dégradé"),
            ServiceStatus.PartialOutage => new("status-partial", "var(--status-degraded)", "▲", "Panne partielle"),
            ServiceStatus.MajorOutage => new("status-major", "var(--status-outage)", "✕", "Panne majeure"),
            ServiceStatus.Maintenance => new("status-maintenance", "var(--status-maintenance)", "⚙", "Maintenance"),
            _ => new("status-unknown", "var(--status-unknown)", "?", "Inconnu"),
        };
    }
}