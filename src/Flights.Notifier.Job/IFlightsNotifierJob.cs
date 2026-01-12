namespace Flights.Notifier.Job;

public interface IFlightsNotifierJob
{
    Task ExecuteAsync();
}