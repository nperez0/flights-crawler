using Flights.Crawler.Models.Queries;

namespace Flights.Crawler.Models.Query;

public class FlightQuerySegment
{
    public required Location Origin { get; set; }
    public required Location Destination { get; set; }
    public DateTime Date { get; set; }
    public FlightQueryDays Days { get; set; }
}
