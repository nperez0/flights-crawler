namespace Flights.Notifier.Job;

public interface IFlightsNotifier
{
    Task NotifyAsync();
}