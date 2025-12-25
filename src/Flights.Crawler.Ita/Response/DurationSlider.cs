using System.Text.Json.Serialization;

namespace Flights.Crawler.Ita.Response;

public class DurationSlider
{
    [JsonPropertyName("groups")]
    public List<DurationGroup>? Groups { get; set; }

    [JsonPropertyName("maxDuration")]
    public int MaxDuration { get; set; }

    [JsonPropertyName("minDuration")]
    public int MinDuration { get; set; }

    [JsonPropertyName("index")]
    public int Index { get; set; }
}

public class DurationGroup
{
    [JsonPropertyName("label")]
    public DurationRange? Label { get; set; }

    [JsonPropertyName("minPrice")]
    public string? MinPrice { get; set; }

    [JsonPropertyName("minPriceInSummary")]
    public bool? MinPriceInSummary { get; set; }
}

public class DurationRange
{
    [JsonPropertyName("end")]
    public int End { get; set; }

    [JsonPropertyName("start")]
    public int Start { get; set; }
}
