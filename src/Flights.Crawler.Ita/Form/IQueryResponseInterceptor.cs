using Flights.Crawler.Ita.Response;

namespace Flights.Crawler.Ita.Form;

public interface IQueryResponseInterceptor
{
    Task<FlightQueryResponse> GetFlightQueryResponseAsync();
}