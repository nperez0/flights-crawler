using Flights.Data.Database;
using Flights.Notifier.Broadcasters;
using Flights.Notifier.Broadcasters.Telegram;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Notifier;

public static class Configuration
{
    public static IServiceCollection RegisterComponents(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TelegramOptions>(configuration.GetSection(TelegramOptions.SectionName));

        services.AddTransient<IBroadcaster, TelegramBroadcaster>();
        services.AddTransient<IFlightsNotifier, FlightsNotifier>();

        services.RegisterDatabaseComponents();

        return services;
    }
}
