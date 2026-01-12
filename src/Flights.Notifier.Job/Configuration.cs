using Flights.Data.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Notifier.Job;

public static class Configuration
{
    public static IServiceCollection RegisterComponents(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDatabaseComponents();
        services.RegisterNotifierComponents();

        services.AddTransient<IFlightsNotifierJob, FlightsNotifierJob>();

        return services;
    }
}
