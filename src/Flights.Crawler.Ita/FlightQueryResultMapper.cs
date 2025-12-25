using Flights.Crawler.Ita.Response;
using Flights.Data.Models.Result;
using Location = Flights.Data.Models.Result.Location;

namespace Flights.Crawler.Ita;

public static class FlightQueryResultMapper
{
    private const int TopSolutionCount = 5;

    public static FlightQueryResult MapToResult(this FlightQueryResponse response, Guid queryId)
    {
        var solutions = response.SolutionList?.Solutions?
            .Take(TopSolutionCount)
            .Select(s => MapSolution(s))
            .ToArray() ?? [];

        return new FlightQueryResult
        {
            Id = Guid.NewGuid(),
            QueryId = queryId,
            Provider = "ITA Matrix",
            Solutions = solutions,
            TotalSolutionCount = response.SolutionCount,
            SearchedAt = DateTime.UtcNow
        };
    }

    private static FlightSolution MapSolution(Solution solution)
    {
        var slices = solution.Itinerary?.Slices?
            .Select(s => MapSlice(s))
            .ToArray() ?? [];

        return new FlightSolution
        {
            Id = solution.Id ?? string.Empty,
            Price = ParsePrice(solution.DisplayTotal),
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
            FlightNumbers = slice.Flights ?? [],
            Cabins = slice.Cabins ?? []
        };
    }

    public static double ParsePrice(string? price)
    {
        if (string.IsNullOrWhiteSpace(price ?? string.Empty))
            return 0d;

        if (double.TryParse(price, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var value))
            return value;

        var cleaned = new string(price!.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
        cleaned = cleaned.Replace(',', '.');

        return double.TryParse(cleaned, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out value)
            ? value
            : 0d;
    }
}
