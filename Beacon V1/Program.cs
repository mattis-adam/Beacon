using System.Text.Json;

var entreprise = "discordstatus";
var url = $"https://{entreprise}.com/api/v2/summary.json";

using var client = new HttpClient();
client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");


var stream = await client.GetStreamAsync(url);
using var doc = JsonDocument.Parse(stream);

var status = doc.RootElement.GetProperty("status");

var indicator = status.GetProperty("indicator").GetString();
if (indicator == "none")
    indicator = "Operational";

var description = status.GetProperty("description").GetString();

Console.WriteLine("Status for Discord.com : ");
Console.WriteLine();
Console.WriteLine($"indicator = {indicator}");
Console.WriteLine($"description = {description}");