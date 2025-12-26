using Flights.Data.Models.Query;
using Flights.Data.Models.Result;

namespace Flights.Crawler;

public interface IFlightsCrawler
{
    string Name { get; }

    Task<FlightQueryResult[]> ExecuteQueriesAsync(FlightQuery[] flightQueries);
}
