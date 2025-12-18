using Flights.Crawler.Models.Queries;
using MongoDB.Driver;

namespace Flights.Crawler.Database.Repositories;

public class FlightQueryRepository : IFlightQueryRepository
{
    private readonly IMongoCollection<FlightQuery> collection;

    public FlightQueryRepository(IMongoClient client)
    {
        var database = client.GetDatabase("flights");
        collection = database.GetCollection<FlightQuery>("queries");
    }

    public async Task<FlightQuery[]> GetAllAsync()
        => await collection.Find(_ => true).ToArrayAsync();
}
