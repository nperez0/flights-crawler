using System.Text.Json.Serialization;

namespace Flights.Crawler.Ita.Response;

public class LocationGroup
{
    [JsonPropertyName("groups")]
    public List<LocationItem>? Groups { get; set; }
}

public class LocationItem
{
    [JsonPropertyName("label")]
    public LocationLabel? Label { get; set; }

    [JsonPropertyName("minPrice")]
    public string? MinPrice { get; set; }

    [JsonPropertyName("minPriceInSummary")]
    public bool? MinPriceInSummary { get; set; }
}

public class LocationLabel
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
