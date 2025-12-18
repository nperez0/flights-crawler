using Flights.Data.Models.Notification;

namespace Flights.Data.Database.Repositories;

public interface IFlightQueryNotificationRepository
{
    Task<FlightQueryNotification[]> GetByQueryIdsAsync(Guid[] queryIds);
    Task SaveAsync(FlightQueryNotification notification);
}
