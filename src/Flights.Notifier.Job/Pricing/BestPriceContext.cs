using Flights.Data.Models.Notification;
using Flights.Data.Models.Query;
using Flights.Data.Models.Result;

namespace Flights.Notifier.Job.Pricing;

public class BestPriceContext(FlightQuery[] queries, FlightQueryResult[] results, FlightQueryPriceDropNotification[] notifications)
{
    public FlightQuery[] Queries { get; } = queries;

    public Dictionary<Guid, FlightQueryResult> LastResults { get; } = results
        .GroupBy(r => r.QueryId)
        .ToDictionary(g => g.Key, g => g.Last());

    public Dictionary<Guid, FlightQueryPriceDropNotification> Notifications { get; } = notifications.ToDictionary(n => n.QueryId);

    public List<BestPrice> BestPrices { get; } = new();
}
