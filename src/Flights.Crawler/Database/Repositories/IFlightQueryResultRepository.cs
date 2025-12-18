using Flights.Crawler.Models.Result;

namespace Flights.Crawler.Database.Repositories;

public interface IFlightQueryResultRepository
{
    Task SaveAsync(FlightQueryResult result);
    Task<FlightQueryResult?> GetByIdAsync(Guid id);
    Task<FlightQueryResult[]> GetAllAsync();
}
