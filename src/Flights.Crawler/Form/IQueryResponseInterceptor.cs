using Flights.Data.Models.Response;

namespace Flights.Crawler.Form;

public interface IQueryResponseInterceptor
{
    Task<FlightQueryResponse> GetFlightQueryResponseAsync();
}