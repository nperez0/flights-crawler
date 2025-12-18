using Flights.Data.Database.Repositories;
using Flights.Data.Models.Notification;
using Flights.Notifier.Broadcasters;

namespace Flights.Notifier;

public class FlightsNotifier(
    IFlightQueryRepository flightQueryRepository,
    IFlightQueryResultRepository flightQueryResultRepository,
    IFlightQueryNotificationRepository flightQueryNotificationRepository,
    IEnumerable<IBroadcaster> broadcasters) : IFlightsNotifier
{
    private IBroadcaster[] broadcasters = [.. broadcasters];

    public async Task NotifyAsync()
    {
        var queries = await flightQueryRepository.GetEnabledQueriesAsync();
        var queryIds = queries.Select(q => q.Id).ToArray();
        var results = await flightQueryResultRepository.GetByQueryIdsAsync(queryIds);
        var notifications = await flightQueryNotificationRepository.GetByQueryIdsAsync(queryIds);
        var context = new NotificationsContext(queries, results, notifications);

        LowerPriceCalculator.CalculateLowerPrices(context);

        await Broadcast(context);
        await PersistNotifications(context);
    }

    private Task Broadcast(NotificationsContext context)
    {
        var tasks = broadcasters
            .SelectMany(b => context.BestPrices.Select(b.BroadcastAsync));

        return Task.WhenAll(tasks);
    }

    private Task PersistNotifications(NotificationsContext context)
    {
        var saveTasks = context.BestPrices
            .Select(x => new FlightQueryNotification
            {
                Id = x.PreviousNotification?.Id ?? Guid.NewGuid(),
                QueryId = x.Query.Id,
                LastNotifiedPrice = x.Price,
                LastResultId = x.BestResult.Id,
                LastNotifiedAt = DateTime.UtcNow
            })
            .Select(flightQueryNotificationRepository.SaveAsync);

        return Task.WhenAll(saveTasks);
    }
}
