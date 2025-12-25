using Flights.Data.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Cleaner.Job;

public static class Configuration
{
    public static IServiceCollection RegisterComponents(this IServiceCollection services)
    {
        services.RegisterDatabaseComponents();

        return services;
    }
}
