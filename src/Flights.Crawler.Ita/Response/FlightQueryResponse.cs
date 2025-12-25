using System.Text.Json.Serialization;

namespace Flights.Crawler.Ita.Response;

public class FlightQueryResponse
{
    public static FlightQueryResponse Empty { get; } = new FlightQueryResponse();

    [JsonPropertyName("carrierStopMatrix")]
    public CarrierStopMatrix? CarrierStopMatrix { get; set; }

    [JsonPropertyName("currencyNotice")]
    public CurrencyNotice? CurrencyNotice { get; set; }

    [JsonPropertyName("durationSliderItinerary")]
    public List<DurationSlider>? DurationSliderItinerary { get; set; }

    [JsonPropertyName("itineraryArrivalTimeRanges")]
    public List<TimeRangeGroup>? ItineraryArrivalTimeRanges { get; set; }

    [JsonPropertyName("itineraryCarrierList")]
    public FilterGroup<CarrierLabel>? ItineraryCarrierList { get; set; }

    [JsonPropertyName("itineraryDepartureTimeRanges")]
    public List<TimeRangeGroup>? ItineraryDepartureTimeRanges { get; set; }

    [JsonPropertyName("itineraryDestinations")]
    public List<LocationGroup>? ItineraryDestinations { get; set; }

    [JsonPropertyName("itineraryOrigins")]
    public List<LocationGroup>? ItineraryOrigins { get; set; }

    [JsonPropertyName("itineraryPriceSlider")]
    public PriceSlider? ItineraryPriceSlider { get; set; }

    [JsonPropertyName("itineraryStopCountList")]
    public FilterGroup<int>? ItineraryStopCountList { get; set; }

    [JsonPropertyName("solutionList")]
    public SolutionList? SolutionList { get; set; }

    [JsonPropertyName("warningsItinerary")]
    public List<WarningGroup>? WarningsItinerary { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("session")]
    public string? Session { get; set; }

    [JsonPropertyName("solutionCount")]
    public int SolutionCount { get; set; }

    [JsonPropertyName("solutionSet")]
    public string? SolutionSet { get; set; }
}
