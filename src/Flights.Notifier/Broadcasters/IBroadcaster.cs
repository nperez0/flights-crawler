using Flights.Notifier.Pricing;

namespace Flights.Notifier.Broadcasters;

public interface IBroadcaster
{
    Task BroadcastAsync(BestPrice[] bestPrices);
}
