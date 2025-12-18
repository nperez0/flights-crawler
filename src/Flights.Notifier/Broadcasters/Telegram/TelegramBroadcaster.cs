using Flights.Data.Models.Query;
using Flights.Data.Models.Response;
using Flights.Data.Models.Result;
using Flights.Notifier.Pricing;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Flights.Notifier.Broadcasters.Telegram;

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

    private string BuildMessage(BestPrice bestPrice)
        => bestPrice.Query.Type switch
        {
            FlightQueryType.MultiCity => BuildMultiCityMessage(bestPrice),
            _ => string.Empty,
        };

    private static string BuildMultiCityMessage(BestPrice bestPrice)
    {
        var message = new StringBuilder();
        var segmentMessages = BuildSegmentMessages(bestPrice);

        message.AppendJoin(Environment.NewLine, segmentMessages);
        message.AppendLine($"{bestPrice.Price:F2}");

        return message.ToString();
    }

    private static IEnumerable<string> BuildSegmentMessages(BestPrice bestPrice)
    {
        var slices = bestPrice.BestResult.Solutions.First().Slices;

        return bestPrice.Query.Segments
            .Select(BuildSegmentMessage(bestPrice.BestResult, slices));
    }

    private static Func<FlightQuerySegment, int, string> BuildSegmentMessage(FlightQueryResult result, FlightSlice[] slices)
        => (segment, index) => BuildSegmentMessage(segment, result, slices[index].DurationMinutes);

    private static string BuildSegmentMessage(FlightQuerySegment segment, FlightQueryResult result, int durationMinutes)
    {
        var duration = TimeSpan.FromMinutes(durationMinutes);
        var formattedDuration = $"{(int)duration.TotalHours:00}h {duration.Minutes:00}m";

        return $"{segment.Origin.Code} -> {segment.Destination.Code} - {segment.Date:yyyy-MM-dd} ({formattedDuration})";
    }
}
