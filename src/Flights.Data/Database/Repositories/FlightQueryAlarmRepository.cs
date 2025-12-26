using Flights.Data.Models.Notification;
using MongoDB.Driver;

namespace Flights.Data.Database.Repositories;

public class FlightQueryAlarmRepository : IFlightQueryAlarmRepository
{
    private readonly IMongoCollection<FlightQueryAlarm> collection;

    public FlightQueryAlarmRepository(IMongoDatabase database)
    {
        collection = database.GetCollection<FlightQueryAlarm>("alarms");
    }

    public async Task<FlightQueryPriceDropAlarm[]> GetAllPriceDropAlarmsAsync()
        => await collection.OfType<FlightQueryPriceDropAlarm>().Find(_ => true).ToListAsync() is var list
            ? [.. list]
            : [];
}
