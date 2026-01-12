using Flights.Data.Models.Notification;
using MongoDB.Driver;

namespace Flights.Data.Database.Repositories;

public class FlightQueryAlertRepository : IFlightQueryAlertRepository
{
    private readonly IMongoCollection<FlightQueryAlert> collection;

    public FlightQueryAlertRepository(IMongoDatabase database)
    {
        collection = database.GetCollection<FlightQueryAlert>("alerts");
    }

    public async Task<FlightQueryPriceDropAlert[]> GetAllPriceDropAlertsAsync()
        => await collection.OfType<FlightQueryPriceDropAlert>().Find(_ => true).ToListAsync() is var list
            ? [.. list]
            : [];
}
