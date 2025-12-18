using System.Text.Json.Serialization;

namespace Flights.Crawler.Models.Response;

public class Segment
{
    [JsonPropertyName("pricings")]
    public List<SegmentPricing>? Pricings { get; set; }
}

public class SegmentPricing
{
    [JsonPropertyName("paxCount")]
    public int PaxCount { get; set; }
}
