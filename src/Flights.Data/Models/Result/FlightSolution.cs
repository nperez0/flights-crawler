namespace Flights.Data.Models.Result;

public class FlightSolution
{
    public required string Id { get; set; }
    public required decimal Price { get; set; }
    public int PassengerCount { get; set; }
    public required FlightSlice[] Slices { get; set; }
}
