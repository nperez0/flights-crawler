namespace Flights.Data.Models.Notification;

public class FlightQueryNotification
{
    public required Guid Id { get; set; }
    public required Guid QueryId { get; set; }
    public required double LastNotifiedPrice { get; set; }
    public required Guid LastResultId { get; set; }
    public DateTime LastNotifiedAt { get; set; }
}
