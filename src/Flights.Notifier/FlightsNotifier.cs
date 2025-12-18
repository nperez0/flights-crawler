using Flights.Data.Database.Repositories;
using Flights.Notifier.Broadcasters;

namespace Flights.Notifier;

public class FlightsNotifier(
    IFlightQueryRepository flightQueryRepository,
    IFlightQueryResultRepository flightQueryResultRepository,
    IEnumerable<IBroadcaster> broadcasters)
{
    private IBroadcaster[] Broadcasters => broadcasters.ToArray();

    public async Task NotifyAsync()
    {
        var queries = await flightQueryRepository.GetEnabledQueriesAsync();
        var queryIds = queries.Select(q => q.Id).ToArray();

        foreach (var query in queries)
        {
        }
    }
}
