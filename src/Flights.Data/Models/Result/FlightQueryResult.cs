using Flights.Data.Models.Query;

namespace Flights.Data.Models.Result;

public class FlightQueryResult
{
    public required Guid Id { get; set; }

    public required Guid QueryId { get; set; }

    public required FlightSolution[] Solutions { get; set; }

    public int TotalSolutionCount { get; set; }

    public DateTime SearchedAt { get; set; }
}
