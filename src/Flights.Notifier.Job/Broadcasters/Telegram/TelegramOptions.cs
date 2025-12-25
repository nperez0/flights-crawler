namespace Flights.Notifier.Job.Broadcasters.Telegram;

public class TelegramOptions
{
    public const string SectionName = "Telegram";

    public required string BotToken { get; set; }
    public required string ChatId { get; set; }
}
