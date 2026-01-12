namespace Flights.Data.Models.Notification;

public class ResetAlert
{
    public int Units { get; set; }

    public ResetAlertType Type { get; set; }

    public bool IsNotificationExpired(DateTime notifiedAt)
    {
        var now = DateTime.UtcNow;

        return Type switch
        {
            ResetAlertType.Hourly => (now - notifiedAt).TotalHours >= Units,
            ResetAlertType.Daily => (now - notifiedAt).TotalDays >= Units,
            ResetAlertType.Weekly => (now - notifiedAt).TotalDays >= Units * 7,
            ResetAlertType.Monthly => (now - notifiedAt).TotalDays >= Units * 30,
            _ => false,
        };
    }
}
