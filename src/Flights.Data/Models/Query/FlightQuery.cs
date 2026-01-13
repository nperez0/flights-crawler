namespace Flights.Data.Models.Query;

public class FlightQuery
{
    public Guid Id { get; set; }
    public FlightQueryType Type { get; set; }
    public bool Disabled { get; set; }
    public required FlightQuerySegment[] Segments { get; set; }
    public Stops Stops { get; set; }
}
