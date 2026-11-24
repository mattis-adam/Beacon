using Beacon_V1;
using System.Text.Json;
using System.Xml.Linq;

var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

List<Service> services;
await using (var file = File.OpenRead("services.json"))
{
    services = await JsonSerializer.DeserializeAsync<List<Service>>(file, options) ?? [];
}
var results = await Task.WhenAll(services.Select(GetStatusAsync));


foreach (var result in results)
    Print(result);

static async Task<StatusResult> GetStatusAsync(Service service)
{
    try
    {
        using var doc = await Client.GetJsonAsync(service.Url);

        var page = doc.RootElement.GetProperty("page");
        var status = doc.RootElement.GetProperty("status");

        var name = page.GetProperty("name").ToString();
        var indicator = status.GetProperty("indicator").ToString();

        if (indicator == "none")
            indicator = "Operational";

        var description = status.GetProperty("description").ToString();

        return new StatusResult
        {
            Name = name,
            Indicator = indicator,
            Description = description,
            Error = null
        };
    }
    catch (Exception ex)
    {

        return new StatusResult
        {
            Name = service.Name,
            Indicator = null,
            Description = null,
            Error = ex.Message
        };
    }


}
static void Print(StatusResult result)
{
    Console.WriteLine(result.Name);

    Console.Write("Indicator: ");
    Console.ForegroundColor = result.Indicator switch
    {
        "Operational" => ConsoleColor.Green,
        "minor" => ConsoleColor.Yellow,
        "major" or "critical" => ConsoleColor.Red,
        _ => ConsoleColor.DarkGray
    };
    Console.WriteLine(result.Indicator);
    Console.ResetColor();

    Console.WriteLine(result.Error is null ? $" Description:{result.Description}" : $"Error: {result.Error}");
    Console.WriteLine();
}