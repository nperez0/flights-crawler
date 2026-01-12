using Flights.Data.Database.Repositories;

namespace Flights.Cleaner.Job.Cleaners;

public class PriceDropNotificationsCleaner(
    IFlightQueryAlertRepository flightQueryAlertRepository,
    IFlightQueryNotificationRepository flightQueryNotificationRepository) 
    : ICleaner
{
    public async Task CleanAsync()
    {
        var alerts = await flightQueryAlertRepository.GetAllPriceDropAlertsAsync();
        var notifications = await flightQueryNotificationRepository.GetAllPriceDropNotificationsAsync();
        var notificationsToDelete = alerts
            .SelectMany(alert => alert.GetExpiredNotifications(notifications))
            .ToArray();

        await flightQueryNotificationRepository.DeleteNotificationsAsync(notificationsToDelete);
    }
}
