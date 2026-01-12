namespace Flights.Data.Models.Result;

public class FlightQueryResult
{
    public static FlightQueryResult Empty => new()
    {
        Id = Guid.Empty,
        QueryId = Guid.Empty,
        Provider = string.Empty,
        Solutions = [],
        TotalSolutionCount = 0,
        SearchedAt = DateTime.MinValue
    };

    public required Guid Id { get; set; }

    public required Guid QueryId { get; set; }

    public required string Provider { get; set; }

    public required FlightSolution[] Solutions { get; set; }

    public int TotalSolutionCount { get; set; }

    public DateTime SearchedAt { get; set; }
}
