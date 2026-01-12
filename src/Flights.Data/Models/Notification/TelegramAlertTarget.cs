namespace Flights.Data.Models.Notification;

public class TelegramAlertTarget : AlertTarget
{
    public override AlertTargetType Type => AlertTargetType.Telegram;

    public required long ChatId { get; set; }
}
