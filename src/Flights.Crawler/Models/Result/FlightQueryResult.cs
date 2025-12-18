using Flights.Crawler.Models.Queries;

namespace Flights.Crawler.Models.Result;

public class FlightQueryResult
{
    public required Guid Id { get; set; }

    public required FlightQuery Query { get; set; }

    public required List<FlightSolution> Solutions { get; set; }

    public int TotalSolutionCount { get; set; }

    public DateTime SearchedAt { get; set; }
}
