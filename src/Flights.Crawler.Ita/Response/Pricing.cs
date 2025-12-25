using System.Text.Json.Serialization;

namespace Flights.Crawler.Ita.Response;

public class Pricing
{
    [JsonPropertyName("ext")]
    public PricingExtension? Ext { get; set; }

    [JsonPropertyName("displayPrice")]
    public string? DisplayPrice { get; set; }
}

public class PricingExtension
{
    [JsonPropertyName("pax")]
    public PassengerCount? Pax { get; set; }
}

public class PassengerCount
{
    [JsonPropertyName("adults")]
    public int Adults { get; set; }

    [JsonPropertyName("children")]
    public int? Children { get; set; }

    [JsonPropertyName("infants")]
    public int? Infants { get; set; }
}
