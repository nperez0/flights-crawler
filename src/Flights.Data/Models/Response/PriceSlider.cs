using System.Text.Json.Serialization;

namespace Flights.Data.Models.Response;

public class PriceSlider
{
    [JsonPropertyName("groups")]
    public List<PriceGroup>? Groups { get; set; }

    [JsonPropertyName("maxPrice")]
    public string? MaxPrice { get; set; }

    [JsonPropertyName("minPrice")]
    public string? MinPrice { get; set; }
}

public class PriceGroup
{
    [JsonPropertyName("label")]
    public PriceRange? Label { get; set; }
}

public class PriceRange
{
    [JsonPropertyName("end")]
    public string? End { get; set; }

    [JsonPropertyName("start")]
    public string? Start { get; set; }
}
