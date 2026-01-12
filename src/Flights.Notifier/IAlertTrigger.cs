namespace Flights.Notifier;

public interface IAlertTrigger
{
    Task TriggerAsync();
}
