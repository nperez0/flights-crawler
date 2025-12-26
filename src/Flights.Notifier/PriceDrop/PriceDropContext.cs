using Flights.Data.Models.Notification;
using Flights.Data.Models.Query;
using Flights.Data.Models.Result;

namespace Flights.Notifier.PriceDrop;

public class PriceDropContext(
    FlightQuery[] queries, 
    FlightQueryPriceDropAlarm[] alarms, 
    FlightQueryResult[] results, 
    FlightQueryPriceDropNotification[] notifications)
{
    public FlightQuery[] Queries { get; } = queries;

    public Dictionary<Guid, FlightQueryPriceDropAlarm[]> Alarms { get; } = alarms
        .GroupBy(x => x.QueryId)
        .ToDictionary(x => x.Key, g => g.ToArray());

    public Dictionary<Guid, FlightQueryResult> LastResults { get; } = results
        .GroupBy(x => x.QueryId)
        .ToDictionary(x => x.Key, g => g.Last());

    public Dictionary<Guid, FlightQueryPriceDropNotification> LastNotifications { get; } = notifications.ToDictionary(x => x.QueryId);

    public List<DroppedPrice> DroppedPrices { get; } = [];
}
