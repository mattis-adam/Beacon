using Beacon.Contracts;

namespace Beacon.Web.Components.Ui
{
    public static class StatusDisplay
    {
        public static (string Css, string Icon, string Label) For(ServiceStatus s) => s switch
        {
            ServiceStatus.Operational => ("status-operational", "✓", "Opérationnel"),
            ServiceStatus.Degraded => ("status-degraded", "!", "Dégradé"),
            ServiceStatus.PartialOutage => ("status-partial", "▲", "Panne partielle"),
            ServiceStatus.MajorOutage => ("status-major", "✕", "Panne majeure"),
            ServiceStatus.Maintenance => ("status-maintenance", "⚙", "Maintenance"),
            _ => ("status-unknown", "?", "Inconnu"),
        };
    }
}