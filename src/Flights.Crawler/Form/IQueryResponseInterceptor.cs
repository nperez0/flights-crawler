using Flights.Crawler.Models.Response;

namespace Flights.Crawler.Form;

public interface IQueryResponseInterceptor
{
    Task<FlightQueryResponse> GetFlightQueryResponseAsync();
}