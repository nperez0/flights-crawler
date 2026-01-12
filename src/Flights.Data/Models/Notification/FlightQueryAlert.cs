namespace Flights.Data.Models.Notification;

public abstract class FlightQueryAlert
{
    public required Guid Id { get; set; }
    public required Guid QueryId { get; set; }
    public required AlertTarget[] Targets { get; set; }
}
