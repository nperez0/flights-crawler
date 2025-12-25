using System.Text.Json.Serialization;

namespace Flights.Crawler.Ita.Response;

public class Carrier
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("shortName")]
    public string? ShortName { get; set; }
}
