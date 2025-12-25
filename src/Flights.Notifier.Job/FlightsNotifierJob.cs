using Flights.Data.Database.Repositories;
using Flights.Data.Models.Notification;
using Flights.Notifier.Job.Broadcasters;
using Flights.Notifier.Job.Pricing;

namespace Flights.Notifier.Job;

public class FlightsNotifierJob(
    IFlightQueryRepository flightQueryRepository,
    IFlightQueryResultRepository flightQueryResultRepository,
    IFlightQueryNotificationRepository flightQueryNotificationRepository,
    IEnumerable<IBroadcaster> broadcasters) : IFlightsNotifierJob
{
    private IBroadcaster[] broadcasters = [.. broadcasters];

    public async Task ExecuteAsync()
    {
        var queries = await flightQueryRepository.GetEnabledQueriesAsync();
        var queryIds = queries.Select(q => q.Id).ToArray();
        var results = await flightQueryResultRepository.GetByQueryIdsAsync(queryIds);
        var notifications = await flightQueryNotificationRepository.GetByQueryIdsAsync(queryIds);
        var context = new BestPriceContext(queries, results, notifications);

        BestPriceDetector.Evaluate(context);

        await Broadcast(context);
        await PersistNotifications(context);
    }

    private Task Broadcast(BestPriceContext context)
    {
        var tasks = broadcasters
            .Select(b => b.BroadcastAsync([.. context.BestPrices]));

        return Task.WhenAll(tasks);
    }

    private Task PersistNotifications(BestPriceContext context)
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
