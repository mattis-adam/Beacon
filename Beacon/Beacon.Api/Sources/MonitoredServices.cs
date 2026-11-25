namespace Beacon.Api.Sources
{
    public static class MonitoredServices
    {
        public static readonly IReadOnlyList<MonitoredService> All =
        [
            new("claude",  "Claude",  "https://status.claude.com"),
            new("discord", "Discord", "https://discordstatus.com"),
            new("gitHub", "GitHub", "https://www.githubstatus.com"),
            new("cloudflare", "Cloudflare", "https://cloudflarestatus.com"),
            new("reddit", "Reddit", "https://redditstatus.com"),
            new("npm", "Npm", "https://status.npmjs.org"),
            new("digitalocean", "DigitalOcean", "https://status.digitalocean.com"),
            new("dropbox", "Dropbox", "https://status.dropbox.com"),
        ];
    }
}