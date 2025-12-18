using Flights.Crawler.Models.Result;
using MongoDB.Driver;

namespace Flights.Crawler.Database.Repositories;

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

    public async Task<FlightQueryResult?> GetByIdAsync(Guid id)
        => await collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<FlightQueryResult[]> GetAllAsync()
        => await collection.Find(_ => true).ToArrayAsync();
}
