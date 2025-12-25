using System.Text.Json.Serialization;

namespace Flights.Crawler.Ita.Response;

public class Airport
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
