using Flights.Data.Models.Notification;
using MongoDB.Driver;

namespace Flights.Data.Database.Repositories;

public class FlightQueryNotificationRepository : IFlightQueryNotificationRepository
{
    private readonly IMongoCollection<FlightQueryNotification> collection;

    public FlightQueryNotificationRepository(IMongoClient client)
    {
        var database = client.GetDatabase("flights");
        collection = database.GetCollection<FlightQueryNotification>("notifications");
    }

    public async Task<FlightQueryNotification[]> GetAllNotificationsAsync()
        => await collection.Find(_ => true).ToListAsync() is var list
            ? [.. list]
            : [];

    public async Task<FlightQueryNotification[]> GetByQueryIdsAsync(Guid[] queryIds)
    {
        if (queryIds.Length == 0)
            return [];

        return await collection.Find(n => queryIds.Contains(n.QueryId)).ToListAsync() is var list
            ? [.. list]
            : [];
    }

    public async Task SaveAsync(FlightQueryNotification notification)
        => await collection.ReplaceOneAsync(
            n => n.Id == notification.Id,
            notification,
            new ReplaceOptions { IsUpsert = true });

    public async Task DeleteNotificationsAsync(FlightQueryNotification[] notifications)
    {
        var notificationIds = notifications.Select(x => x.Id).ToArray();

        await collection.DeleteManyAsync(n => notificationIds.Contains(n.Id));
    }
}
