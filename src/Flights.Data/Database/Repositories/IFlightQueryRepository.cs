using Flights.Data.Models.Query;

namespace Flights.Data.Database.Repositories;

public interface IFlightQueryRepository
{
    Task<FlightQuery[]> GetEnabledQueriesAsync();
    Task<FlightQuery[]> GetEnabledQueriesByQueryIdsAsync(Guid[] queryIds);
}