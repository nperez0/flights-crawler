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
        var results = await flightQueryResultRepository.GetResultsByQueryIdsAsync(queryIds);
        var notifications = await flightQueryNotificationRepository.GetPriceDropNotificationsByQueryIdsAsync(queryIds);
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
            .Select(x => new FlightQueryPriceDropNotification
            {
                Id = x.PreviousNotification?.Id ?? Guid.NewGuid(),
                QueryId = x.Query.Id,
                NotifiedPrice = x.Price,
                ResultId = x.BestResult.Id,
                NotifiedAt = DateTime.UtcNow
            })
            .Select(flightQueryNotificationRepository.SaveAsync);

        return Task.WhenAll(saveTasks);
    }
}
