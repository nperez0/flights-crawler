using Flights.Data.Models.Notification;
using Flights.Data.Models.Query;
using Flights.Data.Models.Result;

namespace Flights.Notifier.PriceDrop;

public class DroppedPrice(
    FlightQuery query, 
    FlightQueryAlarm[] alarms,
    FlightQueryResult bestResult, 
    FlightQueryPriceDropNotification? previousNotification, 
    double price)
{
    public FlightQuery Query { get; } = query;
    public FlightQueryAlarm[] Alarms { get; } = alarms;
    public FlightQueryResult BestResult { get; } = bestResult;
    public FlightQueryPriceDropNotification? PreviousNotification { get; } = previousNotification;
    public double Price { get; } = price;
}
