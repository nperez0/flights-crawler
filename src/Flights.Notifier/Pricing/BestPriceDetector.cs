using Flights.Data.Models.Query;

namespace Flights.Notifier.Pricing;

public static class BestPriceDetector
{
    public static void Evaluate(BestPriceContext context)
    {
        context.Queries.Each(x => Evaluate(x, context));
    }

    public static void Evaluate(FlightQuery query, BestPriceContext context)
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

        context.Notifications.TryGetValue(query.Id, out var previousNotification);

        var previousPrice = previousNotification != null
            ? previousNotification.LastNotifiedPrice
            : (double?)null;

        if (previousPrice.HasValue && bestPrice >= previousPrice.Value)
            return;

        context.BestPrices.Add(new BestPrice(query, latestResult, previousNotification, bestPrice));
    }
}
