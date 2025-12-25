using System.Text.Json.Serialization;

namespace Flights.Crawler.Ita.Response;

public class Distance
{
    [JsonPropertyName("units")]
    public string? Units { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }
}
