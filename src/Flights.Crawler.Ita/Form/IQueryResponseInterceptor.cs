using Flights.Data.Models.Response;

namespace Flights.Crawler.Ita.Form;

public interface IQueryResponseInterceptor
{
    Task<FlightQueryResponse> GetFlightQueryResponseAsync();
}