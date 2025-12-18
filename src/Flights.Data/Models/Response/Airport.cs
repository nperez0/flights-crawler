using System.Text.Json.Serialization;

namespace Flights.Data.Models.Response;

public class Airport
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
