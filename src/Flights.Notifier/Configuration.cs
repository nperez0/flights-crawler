using Flights.Notifier.PriceDrop;
using Flights.Notifier.Senders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Flights.Notifier;

public static class Configuration
{
    public static IServiceCollection RegisterNotifierComponents(this IServiceCollection services)
    {
        
        services.AddScoped<IDroppedPriceSender, DroppedPriceTelegramSender>();

        services.AddTransient<IAlertTrigger, PriceDropAlertTrigger>();

        return services;
    }

    public static IServiceCollection RegisterTelegram(this IServiceCollection services, IConfiguration configuration)
    {
        var token = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");

        services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(token));

        return services;
    }
}
