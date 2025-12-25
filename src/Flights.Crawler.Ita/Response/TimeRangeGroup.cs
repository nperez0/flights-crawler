using System.Text.Json.Serialization;

namespace Flights.Crawler.Ita.Response;

public class TimeRangeGroup
{
    [JsonPropertyName("groups")]
    public List<TimeRangeItem>? Groups { get; set; }
}

public class TimeRangeItem
{
    [JsonPropertyName("label")]
    public TimeRange? Label { get; set; }

    [JsonPropertyName("minPrice")]
    public string? MinPrice { get; set; }

    [JsonPropertyName("minPriceInSummary")]
    public bool? MinPriceInSummary { get; set; }
}

public class TimeRange
{
    [JsonPropertyName("end")]
    public string? End { get; set; }

    [JsonPropertyName("start")]
    public string? Start { get; set; }
}
