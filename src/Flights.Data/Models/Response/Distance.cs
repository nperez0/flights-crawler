using System.Text.Json.Serialization;

namespace Flights.Data.Models.Response;

public class Distance
{
    [JsonPropertyName("units")]
    public string? Units { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; }
}
