namespace Flights.Data.Models.Query;

public class FlightQuerySegment
{
    public required Location Origin { get; set; }
    public required Location Destination { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly? End { get; set; }
    public FlightQueryDays Days { get; set; }
}
