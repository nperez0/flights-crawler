namespace Flights.Data.Models.Notification;

public class FlightQueryPriceDropNotification : FlightQueryNotification
{
    public required double NotifiedPrice { get; set; }
}
