namespace Flights.Data.Models.Notification;

public abstract class FlightQueryAlarm
{
    public required Guid Id { get; set; }
    public required Guid QueryId { get; set; }
    public required AlarmTarget[] Targets { get; set; }
}
