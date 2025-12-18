using System.Text.Json.Serialization;

namespace Flights.Data.Models.Response;

public class Itinerary
{
    [JsonPropertyName("ext")]
    public ItineraryExtension? Ext { get; set; }

    [JsonPropertyName("slices")]
    public List<Slice>? Slices { get; set; }

    [JsonPropertyName("carriers")]
    public List<Carrier>? Carriers { get; set; }

    [JsonPropertyName("singleCarrier")]
    public Carrier? SingleCarrier { get; set; }

    [JsonPropertyName("distance")]
    public Distance? Distance { get; set; }
}

public class ItineraryExtension
{
    [JsonPropertyName("dominantCarrier")]
    public Carrier? DominantCarrier { get; set; }

    [JsonPropertyName("warnings")]
    public ItineraryWarnings? Warnings { get; set; }
}

public class ItineraryWarnings
{
    [JsonPropertyName("changeOfAirportTrip")]
    public bool? ChangeOfAirportTrip { get; set; }
}
