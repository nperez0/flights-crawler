using System.Text.Json.Serialization;

namespace Flights.Crawler.Ita.Response;

public class Solution
{
    [JsonPropertyName("ext")]
    public SolutionExtension? Ext { get; set; }

    [JsonPropertyName("itinerary")]
    public Itinerary? Itinerary { get; set; }

    [JsonPropertyName("pricings")]
    public List<Pricing>? Pricings { get; set; }

    [JsonPropertyName("displayTotal")]
    public string? DisplayTotal { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("passengerCount")]
    public int PassengerCount { get; set; }
}

public class SolutionExtension
{
    [JsonPropertyName("price")]
    public string? Price { get; set; }

    [JsonPropertyName("pricePerMile")]
    public string? PricePerMile { get; set; }

    [JsonPropertyName("totalPrice")]
    public string? TotalPrice { get; set; }
}
