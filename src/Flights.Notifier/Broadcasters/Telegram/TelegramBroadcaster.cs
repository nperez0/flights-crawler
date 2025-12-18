using Flights.Data.Models.Query;
using Microsoft.Extensions.Options;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Flights.Notifier.Broadcasters.Telegram;

public class TelegramBroadcaster(IOptions<TelegramOptions> options) : IBroadcaster
{
    private readonly TelegramOptions telegramOptions = options.Value;

    public async Task BroadcastAsync(BestPrice bestPrice)
    {
        var bot = new TelegramBotClient(telegramOptions.BotToken);

        await bot.SendMessage(
            chatId: telegramOptions.ChatId,
            text: BuildMessage(bestPrice),
            parseMode: ParseMode.None,
            cancellationToken: CancellationToken.None
        );
    }

    private string BuildMessage(BestPrice bestPrice)
    {
        switch (bestPrice.Query.Type)
        {
            case FlightQueryType.MultiCity:
                return BuildMultiCityMessage(bestPrice);
            default:
                return string.Empty;
        }
    }

    private static string BuildMultiCityMessage(BestPrice bestPrice)
    {
        var message = new StringBuilder();

        message.AppendJoin(Environment.NewLine, bestPrice.Query.Segments.Select(BuildSegmentMessage));
        message.AppendLine(bestPrice.Price);

        return message.ToString();
    }

    private static string BuildSegmentMessage(FlightQuerySegment segment)
        => $"{segment.Origin.Code} -> {segment.Destination.Code} - {segment.Date:yyyy-MM-dd}";
}
