using Flights.Data.Models.Result;
using MongoDB.Driver;

namespace Flights.Data.Database.Repositories;

public class FlightQueryResultRepository : IFlightQueryResultRepository
{
    private readonly IMongoCollection<FlightQueryResult> collection;

    public FlightQueryResultRepository(IMongoClient client)
    {
        var database = client.GetDatabase("flights");
        collection = database.GetCollection<FlightQueryResult>("results");
    }

    public async Task SaveAsync(FlightQueryResult result)
        => await collection.InsertOneAsync(result);

    public async Task<FlightQueryResult?> GetByQueryIdAsync(Guid[] id)
        => await collection.Find(r => id.Contains(r.QueryId))
        .SortByDescending(r => r.SearchedAt)
        .FirstOrDefaultAsync();
}
