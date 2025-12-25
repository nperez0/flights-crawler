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

    public async Task<FlightQueryResult[]> GetByQueryIdsAsync(Guid[] queryIds)
    {
        if (queryIds.Length == 0)
            return [];

        var list = await collection
            .Find(r => queryIds.Contains(r.QueryId))
            .ToListAsync();

        return [.. list];
    }

    public async Task<FlightQueryResult[]> GetResultsOlderThanAsync(DateTime cutoffDate)
        => await collection.Find(r => r.SearchedAt < cutoffDate).ToListAsync() is var list
            ? [.. list]
            : [];

    public async Task SaveAsync(FlightQueryResult[] results)
        => await collection.InsertManyAsync(results);

    public async Task DeleteResultsAsync(FlightQueryResult[] results)
    {
        var resultIds = results.Select(x => x.Id).ToArray();

        await collection.DeleteManyAsync(n => resultIds.Contains(n.Id));
    }
}
