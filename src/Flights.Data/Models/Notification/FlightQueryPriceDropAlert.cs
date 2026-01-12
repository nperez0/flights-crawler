namespace Flights.Data.Models.Notification;

public class FlightQueryPriceDropAlert : FlightQueryAlert
{
    public ResetAlert? Reset { get; set; }

    public FlightQueryPriceDropNotification[] GetExpiredNotifications(FlightQueryPriceDropNotification[] notifications) 
        => [.. notifications
            .Where(x => x.QueryId == QueryId)
            .Where(x => Reset?.IsNotificationExpired(x.NotifiedAt) == true)];
}
