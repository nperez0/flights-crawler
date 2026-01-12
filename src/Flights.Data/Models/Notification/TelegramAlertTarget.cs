namespace Flights.Data.Models.Notification;

public class TelegramAlarmTarget : AlertTarget
{
    public override AlertTargetType Type => AlertTargetType.Telegram;

    public required long ChatId { get; set; }
}
