using Flights.Data.Models.Query;

namespace Flights.Notifier;

public static class LowerPriceCalculator
{
    public static void CalculateLowerPrices(NotificationsContext context)
    {
        context.Queries.Each(x => CalculateLowerPrice(x, context));
    }

    public static void CalculateLowerPrice(FlightQuery query, NotificationsContext context)
    {
        context.LastResults.TryGetValue(query.Id, out var latestResult);

        if (latestResult is { Solutions.Count: 0 })
            return;

        var bestSolution = latestResult!.Solutions
            .OrderBy(s => s.Price.ParsePrice())
            .FirstOrDefault();

        if (bestSolution == null)
            return;

        var bestPrice = bestSolution.Price.ParsePrice();

        context.Notifications.TryGetValue(query.Id, out var previousNotification);

        var previousPrice = previousNotification != null
            ? previousNotification.LastNotifiedPrice.ParsePrice()
            : (decimal?)null;

        if (previousPrice.HasValue && bestPrice >= previousPrice.Value)
            return;

        context.BestPrices.Add(new BestPrice(query, latestResult, previousNotification, bestSolution.Price));
    }
}
