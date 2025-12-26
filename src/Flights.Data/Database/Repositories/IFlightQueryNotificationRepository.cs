using Flights.Data.Models.Notification;

namespace Flights.Data.Database.Repositories;

public interface IFlightQueryNotificationRepository
{
    Task<FlightQueryPriceDropNotification[]> GetPriceDropNotificationsByQueryIdsAsync(Guid[] queryIds);
    Task<FlightQueryPriceDropNotification[]> GetAllPriceDropNotificationsAsync();
    Task SaveAsync(FlightQueryNotification notification);
    Task DeleteNotificationsAsync(FlightQueryNotification[] notifications);
}
