using System.Text.Json.Serialization;

namespace Flights.Crawler.Models.Response;

public class FilterGroup<T>
{
    [JsonPropertyName("groups")]
    public List<FilterItem<T>>? Groups { get; set; }
}

public class FilterItem<T>
{
    [JsonPropertyName("label")]
    public T? Label { get; set; }

    [JsonPropertyName("minPrice")]
    public string? MinPrice { get; set; }

    [JsonPropertyName("minPriceInSummary")]
    public bool? MinPriceInSummary { get; set; }
}

public class CarrierLabel
{
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("shortName")]
    public string? ShortName { get; set; }
}
