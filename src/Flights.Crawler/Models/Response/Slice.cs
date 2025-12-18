using System.Text.Json.Serialization;

namespace Flights.Crawler.Models.Response;

public class Slice
{
    [JsonPropertyName("origin")]
    public Airport? Origin { get; set; }

    [JsonPropertyName("destination")]
    public Airport? Destination { get; set; }

    [JsonPropertyName("arrival")]
    public string? Arrival { get; set; }

    [JsonPropertyName("departure")]
    public string? Departure { get; set; }

    [JsonPropertyName("flights")]
    public List<string>? Flights { get; set; }

    [JsonPropertyName("stops")]
    public List<Airport>? Stops { get; set; }

    [JsonPropertyName("cabins")]
    public List<string>? Cabins { get; set; }

    [JsonPropertyName("segments")]
    public List<Segment>? Segments { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("ext")]
    public SliceExtension? Ext { get; set; }
}

public class SliceExtension
{
    [JsonPropertyName("warnings")]
    public SliceWarnings? Warnings { get; set; }
}

public class SliceWarnings
{
    [JsonPropertyName("types")]
    public List<string>? Types { get; set; }

    [JsonPropertyName("overnight")]
    public bool? Overnight { get; set; }
}
