namespace Flights.Data.Models.Notification;

public abstract class AlertTarget
{
    public abstract AlertTargetType Type { get; }
}
