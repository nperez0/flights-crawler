using Flights.Data.Models.Reference;
using MongoDB.Driver;

namespace Flights.Data.Database.Repositories;

public class AirportRepository : IAirportRepository
{
    private static Dictionary<string, Airport> airportsCache = new();

    private readonly IMongoCollection<Airport> collection;

    public AirportRepository(IMongoDatabase database)
    {
        collection = database.GetCollection<Airport>("airports");
    }

    public async Task<Dictionary<string, Airport>> GetAllAirportsAsync()
    {
        if (airportsCache.Count > 0)
            return airportsCache;

        var airports = await collection.Find(_ => true).ToArrayAsync();

        airportsCache = airports.ToDictionary(a => a.Code);

        return airportsCache;
    }
}
