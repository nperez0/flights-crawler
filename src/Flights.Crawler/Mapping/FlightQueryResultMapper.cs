using Flights.Crawler.Models.Queries;
using Flights.Crawler.Models.Response;
using Flights.Crawler.Models.Result;
using Location = Flights.Crawler.Models.Result.Location;

namespace Flights.Crawler.Mapping;

public static class FlightQueryResultMapper
{
    private const int TopSolutionCount = 5;

    public static FlightQueryResult MapToResult(this FlightQueryResponse response, FlightQuery query)
    {
        var solutions = response.SolutionList?.Solutions?
            .Take(TopSolutionCount)
            .Select(MapSolution)
            .ToList() ?? [];

        return new FlightQueryResult
        {
            Id = Guid.NewGuid(),
            Query = query,
            Solutions = solutions,
            TotalSolutionCount = response.SolutionCount,
            SearchedAt = DateTime.UtcNow
        };
    }

    private static FlightSolution MapSolution(Solution solution)
    {
        var slices = solution.Itinerary?.Slices?
            .Select(MapSlice)
            .ToList() ?? [];

        return new FlightSolution
        {
            Id = solution.Id ?? string.Empty,
            Price = solution.DisplayTotal ?? "0",
            PassengerCount = solution.PassengerCount,
            Slices = slices
        };
    }

    private static FlightSlice MapSlice(Slice slice)
    {
        DateTime departureTime = DateTime.TryParse(slice.Departure, out var dept) 
            ? dept 
            : DateTime.MinValue;
            
        DateTime arrivalTime = DateTime.TryParse(slice.Arrival, out var arr) 
            ? arr 
            : DateTime.MinValue;

        return new FlightSlice
        {
            Origin = new Location
            {
                Code = slice.Origin?.Code ?? string.Empty,
                Name = slice.Origin?.Name ?? string.Empty
            },
            Destination = new Location
            {
                Code = slice.Destination?.Code ?? string.Empty,
                Name = slice.Destination?.Name ?? string.Empty
            },
            DepartureTime = departureTime,
            ArrivalTime = arrivalTime,
            DurationMinutes = slice.Duration,
            StopCount = slice.Stops?.Count ?? 0,
            Cabins = slice.Cabins ?? []
        };
    }
}
