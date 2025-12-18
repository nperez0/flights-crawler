using Flights.Data.Models.Result;

namespace Flights.Data.Database.Repositories;

public interface IFlightQueryResultRepository
{
    Task SaveAsync(FlightQueryResult result);

    Task<FlightQueryResult?> GetByQueryIdAsync(Guid[] id);
}
