namespace Flights.Notifier.Job;

public interface IFlightsNotifierJob
{
    ValueTask ExecuteAsync();
}