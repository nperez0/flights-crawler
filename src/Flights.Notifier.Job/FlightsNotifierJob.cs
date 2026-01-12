namespace Flights.Notifier.Job;

public class FlightsNotifierJob(IEnumerable<IAlertTrigger> alertTriggers) : IFlightsNotifierJob
{
    public async ValueTask ExecuteAsync()
    {
        foreach (var trigger in alertTriggers)
            await trigger.TriggerAsync();
    }
}
