using Flights.Data.Models.Result;

namespace Flights.Data.Database.Repositories;

public interface IFlightQueryResultRepository
{
    Task<FlightQueryResult[]> GetByQueryIdsAsync(Guid[] ids);
    Task SaveAsync(FlightQueryResult[] results);
}
