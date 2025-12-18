namespace Flights.Notifier.Broadcasters;

public interface IBroadcaster
{
    Task BroadcastAsync(string message);
}
