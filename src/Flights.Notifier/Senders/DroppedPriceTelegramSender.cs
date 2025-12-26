using Flights.Data.Database.Repositories;
using Flights.Data.Models.Notification;
using Flights.Data.Models.Reference;
using Flights.Data.Models.Result;
using Flights.Notifier.PriceDrop;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Flights.Notifier.Senders;

public class DroppedPriceTelegramSender(
    ITelegramBotClient telegramBotClient,
    IAirportRepository airportRepository) 
    : IDroppedPriceSender
{
    public AlarmTargetType Type => AlarmTargetType.Telegram;

    public async Task NotifyDroppedPriceAsync(DroppedPrice droppedPrice, AlarmTarget target)
    {
        var telegramNotificator = (TelegramAlarmTarget)target;
        var airports = await airportRepository.GetAllAirportsAsync();

        await telegramBotClient.SendMessage(
                chatId: telegramNotificator.ChatId,
                text: BuildMessage(droppedPrice, airports),
                parseMode: ParseMode.None,
                cancellationToken: CancellationToken.None
            );
    }

    public static string BuildMessage(DroppedPrice droppedPrice, Dictionary<string, Airport> airports)
    {
        var message = new StringBuilder();
        var segmentMessages = BuildSegmentMessages(droppedPrice, airports);

        message.AppendLine(droppedPrice.BestResult.Provider);
        message.AppendJoin(Environment.NewLine, segmentMessages);
        message.AppendLine();
        message.Append($"{droppedPrice.Price:F2}");

        return message.ToString();
    }

    private static IEnumerable<string> BuildSegmentMessages(DroppedPrice bestPrice, Dictionary<string, Airport> airports)
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
