using Flights.Notifier.Job.Pricing;

namespace Flights.Notifier.Job.Broadcasters;

public interface IBroadcaster
{
    Task BroadcastAsync(BestPrice[] bestPrices);
}
