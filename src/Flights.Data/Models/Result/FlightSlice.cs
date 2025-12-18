namespace Flights.Data.Models.Result;

public class FlightSlice
{
    public required Location Origin { get; set; }
    public required Location Destination { get; set; }
    public required DateTime DepartureTime { get; set; }
    public required DateTime ArrivalTime { get; set; }
    public int DurationMinutes { get; set; }
    public int StopCount { get; set; }
    public List<string> FlightNumbers { get; set; } = [];
    public List<string> Cabins { get; set; } = [];
}
