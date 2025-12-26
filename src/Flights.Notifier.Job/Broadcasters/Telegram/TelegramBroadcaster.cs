using Flights.Data.Database.Repositories;
using Flights.Data.Models.Reference;
using Flights.Data.Models.Result;
using Flights.Notifier.Job.Pricing;
using Microsoft.Extensions.Options;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Flights.Notifier.Job.Broadcasters.Telegram;

public class TelegramBroadcaster(
    IOptions<TelegramOptions> options,
    IAirportRepository airportRepository) : IBroadcaster
{
    private readonly TelegramOptions telegramOptions = options.Value;

    public async Task BroadcastAsync(BestPrice[] bestPrices)
    {
        var bot = new TelegramBotClient(telegramOptions.BotToken);
        var airports = await airportRepository.GetAllAirportsAsync();

        foreach (var bestPrice in bestPrices)
        {
            await bot.SendMessage(
                chatId: telegramOptions.ChatId,
                text: BuildMessage(bestPrice, airports),
                parseMode: ParseMode.None,
                cancellationToken: CancellationToken.None
            );
        }
    }

    public static string BuildMessage(BestPrice bestPrice, Dictionary<string, Airport> airports)
    {
        var message = new StringBuilder();
        var segmentMessages = BuildSegmentMessages(bestPrice, airports);

        message.AppendLine(bestPrice.BestResult.Provider);
        message.AppendJoin(Environment.NewLine, segmentMessages);
        message.AppendLine();
        message.Append($"{bestPrice.Price:F2}");

        return message.ToString();
    }

    private static IEnumerable<string> BuildSegmentMessages(BestPrice bestPrice, Dictionary<string, Airport> airports)
    {
        var slices = bestPrice.BestResult.Solutions.First().Slices;

        return slices
            .Select(x => BuildSegmentMessage(x, airports));
    }

    private static string BuildSegmentMessage(FlightSlice slice, Dictionary<string, Airport> airports)
    {
        var duration = TimeSpan.FromMinutes(slice.DurationMinutes);
        var formattedDuration = $"{(int)duration.TotalHours:00}h {duration.Minutes:00}m";
        var origin = airports[slice.Origin.Code];
        var destination = airports[slice.Destination.Code];

        return $"{origin.City} ({origin.Code}) -> {destination.City} ({destination.Code}) - {slice.DepartureTime:yyyy-MM-dd} ({formattedDuration})";
    }
}
