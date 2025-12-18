using System.Text.Json.Serialization;

namespace Flights.Data.Models.Response;

public class WarningGroup
{
    [JsonPropertyName("groups")]
    public List<WarningItem>? Groups { get; set; }
}

public class WarningItem
{
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("minPrice")]
    public string? MinPrice { get; set; }

    [JsonPropertyName("minPriceInSummary")]
    public bool? MinPriceInSummary { get; set; }
}
