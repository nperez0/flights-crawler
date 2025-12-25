using Flights.Data.Models.Query;
using Flights.Data.Models.Result;
using Flights.Notifier.Job.Pricing;
using Microsoft.Extensions.Options;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Flights.Notifier.Job.Broadcasters.Telegram;

public class TelegramBroadcaster(IOptions<TelegramOptions> options) : IBroadcaster
{
    private readonly TelegramOptions telegramOptions = options.Value;

    public async Task BroadcastAsync(BestPrice[] bestPrices)
    {
        var bot = new TelegramBotClient(telegramOptions.BotToken);

        foreach (var bestPrice in bestPrices)
        {
            await bot.SendMessage(
                chatId: telegramOptions.ChatId,
                text: BuildMessage(bestPrice),
                parseMode: ParseMode.None,
                cancellationToken: CancellationToken.None
            );
        }
    }

    public static string BuildMessage(BestPrice bestPrice)
    {
        var message = new StringBuilder();
        var segmentMessages = BuildSegmentMessages(bestPrice);

        message.AppendJoin(Environment.NewLine, segmentMessages);
        message.AppendLine();
        message.Append($"{bestPrice.Price:F2}");

        return message.ToString();
    }

    private static IEnumerable<string> BuildSegmentMessages(BestPrice bestPrice)
    {
        var slices = bestPrice.BestResult.Solutions.First().Slices;

        return bestPrice.Query.Segments
            .Select(BuildSegmentMessage(slices));
    }

    private static Func<FlightQuerySegment, int, string> BuildSegmentMessage(FlightSlice[] slices)
        => (segment, index) => BuildSegmentMessage(segment, slices[index]);

    private static string BuildSegmentMessage(FlightQuerySegment segment, FlightSlice slice)
    {
        var duration = TimeSpan.FromMinutes(slice.DurationMinutes);
        var formattedDuration = $"{(int)duration.TotalHours:00}h {duration.Minutes:00}m";

        return $"{segment.Origin.Code} -> {segment.Destination.Code} - {slice.DepartureTime:yyyy-MM-dd} ({formattedDuration})";
    }
}
