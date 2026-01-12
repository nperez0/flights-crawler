namespace Flights.Notifier;

public interface IAlarmTrigger
{
    Task TriggerAsync();
}
