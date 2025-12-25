using Flights.Data.Models.Result;

namespace Flights.Data.Database.Repositories;

public interface IFlightQueryResultRepository
{
    
    Task<FlightQueryResult[]> GetByQueryIdsAsync(Guid[] ids);
    Task<FlightQueryResult[]> GetResultsOlderThanAsync(DateTime cutoffDate);
    Task SaveAsync(FlightQueryResult[] results);
    Task DeleteResultsAsync(FlightQueryResult[] results);
}
