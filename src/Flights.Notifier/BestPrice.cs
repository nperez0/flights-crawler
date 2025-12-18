using Flights.Data.Models.Notification;
using Flights.Data.Models.Query;
using Flights.Data.Models.Result;

namespace Flights.Notifier;

public class BestPrice(FlightQuery query, FlightQueryResult bestResult, FlightQueryNotification? previousNotification, double price)
{
    public FlightQuery Query { get; } = query;
    public FlightQueryResult BestResult { get; } = bestResult;
    public FlightQueryNotification? PreviousNotification { get; } = previousNotification;
    public double Price { get; } = price;
}
