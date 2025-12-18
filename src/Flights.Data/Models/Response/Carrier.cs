using System.Text.Json.Serialization;

namespace Flights.Data.Models.Response;

public class Carrier
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("shortName")]
    public string? ShortName { get; set; }
}
