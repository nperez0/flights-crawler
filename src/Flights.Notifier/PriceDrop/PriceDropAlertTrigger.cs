using Flights.Core.Extensions;
using Flights.Data.Database.Repositories;
using Flights.Data.Models.Notification;
using Flights.Data.Models.Query;

namespace Flights.Notifier.PriceDrop;

public class PriceDropAlertTrigger(
    IFlightQueryAlertRepository flightQueryAlertRepository,
    IFlightQueryRepository flightQueryRepository,
    IFlightQueryResultRepository flightQueryResultRepository,
    IFlightQueryNotificationRepository flightQueryNotificationRepository,
    IEnumerable<IDroppedPriceSender> droppedPriceSenders) 
    : IAlertTrigger
{
    private readonly Dictionary<AlertTargetType, IDroppedPriceSender> droppedPriceSenders = droppedPriceSenders.ToDictionary(x => x.Type);

    public async Task TriggerAsync()
    {
        var alerts = await flightQueryAlertRepository.GetAllPriceDropAlertsAsync();
        var queryIds = alerts.Select(q => q.Id).Distinct().ToArray();
        var queries = await flightQueryRepository.GetEnabledQueriesByQueryIdsAsync(queryIds);
        queryIds = [.. queries.Select(q => q.Id)];
        var results = await flightQueryResultRepository.GetResultsByQueryIdsAsync(queryIds);
        var notifications = await flightQueryNotificationRepository.GetPriceDropNotificationsByQueryIdsAsync(queryIds);
        var context = new PriceDropContext(queries, alerts, results, notifications);

        context.Queries.Each(x => DectectPriceDrop(x, context));

        await NotifyDroppedPrices(context);
        await PersistNotifications(context);
    }

    public static void DectectPriceDrop(FlightQuery query, PriceDropContext context)
    {
        context.LastResults.TryGetValue(query.Id, out var latestResult);

        if (latestResult is null || latestResult is { Solutions.Length: 0 })
            return;

        var bestSolution = latestResult!.Solutions
            .OrderBy(s => s.Price)
            .FirstOrDefault();

        if (bestSolution == null)
            return;

        var bestPrice = bestSolution.Price;

        context.LastNotifications.TryGetValue(query.Id, out var previousNotification);

        var previousPrice = previousNotification != null
            ? previousNotification.NotifiedPrice
            : (double?)null;

        if (previousPrice.HasValue && bestPrice >= previousPrice.Value)
            return;

        context.DroppedPrices.Add(new DroppedPrice(query, context.Alerts[query.Id], latestResult, previousNotification, bestPrice));
    }

    private async Task NotifyDroppedPrices(PriceDropContext context)
    {
        if (context.DroppedPrices.Count == 0)
            return;

        var tasks = context
            .DroppedPrices
            .Select(NotifyDroppedPrices);

        await Task.WhenAll(tasks);
    }

    private async Task NotifyDroppedPrices(DroppedPrice droppedPrice)
    {
        var tasks = droppedPrice
            .Alerts
            .SelectMany(x => x.Targets)
            .Select(x => droppedPriceSenders[x.Type].NotifyDroppedPriceAsync(droppedPrice, x));

        await Task.WhenAll(tasks);
    }

    private Task PersistNotifications(PriceDropContext context)
    {
        var saveTasks = context.DroppedPrices
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
