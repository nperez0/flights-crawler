using Flights.Data.Models.Query;
using MongoDB.Driver;

namespace Flights.Data.Database.Repositories;

public class FlightQueryRepository : IFlightQueryRepository
{
    private readonly IMongoCollection<FlightQuery> collection;

    public FlightQueryRepository(IMongoClient client)
    {
        var database = client.GetDatabase("flights");
        collection = database.GetCollection<FlightQuery>("queries");
    }

    public async Task<FlightQuery[]> GetEnabledQueriesAsync()
    {
        var queries = await collection.Find(_ => true).ToArrayAsync();

        return queries
            .Where(q => !q.Disabled)
            .ToArray();
    }
}
