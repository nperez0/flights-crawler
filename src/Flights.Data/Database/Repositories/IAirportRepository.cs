using Flights.Data.Models.Reference;

namespace Flights.Data.Database.Repositories;

public interface IAirportRepository
{
    Task<Dictionary<string, Airport>> GetAllAirportsAsync();
}
