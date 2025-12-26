namespace Flights.Data.Models.Notification;

public class TelegramAlarmTarget : AlarmTarget
{
    public override AlarmTargetType Type => AlarmTargetType.Telegram;

    public required long ChatId { get; set; }
}
