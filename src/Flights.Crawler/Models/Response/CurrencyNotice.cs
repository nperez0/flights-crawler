using System.Text.Json.Serialization;

namespace Flights.Crawler.Models.Response;

public class CurrencyNotice
{
    [JsonPropertyName("ext")]
    public CurrencyExtension? Ext { get; set; }
}

public class CurrencyExtension
{
    [JsonPropertyName("price")]
    public string? Price { get; set; }
}
