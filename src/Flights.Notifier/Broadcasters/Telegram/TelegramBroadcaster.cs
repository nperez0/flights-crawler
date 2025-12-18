using Microsoft.Extensions.Options;
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
            text: "",
            parseMode: ParseMode.None,
            cancellationToken: CancellationToken.None
        );
    }
}
