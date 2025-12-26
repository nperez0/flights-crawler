using Flights.Data.Database.Repositories;

namespace Flights.Cleaner.Job.Cleaners;

public class PriceDropNotificationsCleaner(IFlightQueryNotificationRepository flightQueryNotificationRepository) : ICleaner
{
    public async Task CleanAsync()
    {
        var oldNotifications = await flightQueryNotificationRepository.GetAllPriceDropNotificationsAsync();

        await flightQueryNotificationRepository.DeleteNotificationsAsync(oldNotifications);
    }
}
