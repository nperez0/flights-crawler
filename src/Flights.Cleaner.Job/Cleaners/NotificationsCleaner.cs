using Flights.Data.Database.Repositories;

namespace Flights.Cleaner.Job.Cleaners;

public class NotificationsCleaner(IFlightQueryNotificationRepository flightQueryNotificationRepository) : ICleaner
{
    public async Task CleanAsync()
    {
        var oldNotifications = await flightQueryNotificationRepository.GetAllNotificationsAsync();

        await flightQueryNotificationRepository.DeleteNotificationsAsync(oldNotifications);
    }
}
