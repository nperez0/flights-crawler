using Flights.Cleaner.Job.Cleaners;
using Flights.Data.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Cleaner.Job;

public static class Configuration
{
    public static IServiceCollection RegisterComponents(this IServiceCollection services)
    {
        services.RegisterDatabaseComponents();

        services.AddSingleton<ICleanerJob, CleanerJob>();

        services.AddTransient<ICleaner, PriceDropNotificationsCleaner>();
        services.AddTransient<ICleaner, OldQueryResultsCleaner>();

        return services;
    }
}
