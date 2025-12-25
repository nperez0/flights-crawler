using Flights.Data.Database;
using Flights.Notifier.Job.Broadcasters;
using Flights.Notifier.Job.Broadcasters.Telegram;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Notifier.Job;

public static class Configuration
{
    public static IServiceCollection RegisterComponents(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDatabaseComponents();

        services.Configure<TelegramOptions>(configuration.GetSection(TelegramOptions.SectionName));

        services.AddTransient<IBroadcaster, TelegramBroadcaster>();
        services.AddTransient<IFlightsNotifierJob, FlightsNotifierJob>();

        return services;
    }
}
