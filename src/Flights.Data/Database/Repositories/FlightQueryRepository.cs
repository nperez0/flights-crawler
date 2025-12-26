using Flights.Data.Models.Query;
using MongoDB.Driver;

namespace Flights.Data.Database.Repositories;

public class FlightQueryRepository : IFlightQueryRepository
{
    private readonly IMongoCollection<FlightQuery> collection;

    public FlightQueryRepository(IMongoDatabase database)
    {
        collection = database.GetCollection<FlightQuery>("queries");
    }

    public async Task<FlightQuery[]> GetEnabledQueriesAsync()
    {
        var queries = await collection.Find(_ => true).ToArrayAsync();

        return [.. queries.Where(q => !q.Disabled)];
    }

    public async Task<FlightQuery[]> GetEnabledQueriesByQueryIdsAsync(Guid[] queryIds)
    {
        var queries = await collection
            .Find(q => queryIds.Contains(q.Id) && !q.Disabled)
            .ToArrayAsync();

        return [.. queries.Where(q => !q.Disabled)];
    }
}
