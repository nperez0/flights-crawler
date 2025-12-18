using Flights.Data.Models.Notification;
using Flights.Data.Models.Query;
using Flights.Data.Models.Result;

namespace Flights.Notifier;

public class BestPrice(FlightQuery query, FlightQueryResult bestResult, FlightQueryNotification? previousNotification, decimal price)
{
    public FlightQuery Query { get; } = query;
    public FlightQueryResult BestResult { get; } = bestResult;
    public FlightQueryNotification? PreviousNotification { get; } = previousNotification;
    public decimal Price { get; } = price;
}
