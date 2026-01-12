namespace Flights.Notifier.Job;

public class FlightsNotifierJob(IEnumerable<IAlertTrigger> alertTriggers) : IFlightsNotifierJob
{
    public async Task ExecuteAsync()
    {
        foreach (var trigger in alertTriggers)
            await trigger.TriggerAsync();
    }
}
