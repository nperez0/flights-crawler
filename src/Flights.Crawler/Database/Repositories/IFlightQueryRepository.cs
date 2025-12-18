using Flights.Crawler.Models.Queries;

namespace Flights.Crawler.Database.Repositories;

public interface IFlightQueryRepository
{
    Task<FlightQuery[]> GetAllAsync();
}