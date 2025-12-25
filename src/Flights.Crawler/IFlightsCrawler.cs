using Flights.Data.Models.Query;
using Flights.Data.Models.Result;

namespace Flights.Crawler;

public interface IFlightsCrawler
{
    Task<FlightQueryResult[]> ExecuteQueriesAsync(FlightQuery[] flightQueries);
}
