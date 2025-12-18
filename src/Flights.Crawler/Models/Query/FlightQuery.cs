using Flights.Crawler.Models.Query;

namespace Flights.Crawler.Models.Queries;

public class FlightQuery
{
    public Guid Id { get; set; }
    public FlightQueryType Type { get; set; }
    public required FlightQuerySegment[] Segments { get; set; }
}
