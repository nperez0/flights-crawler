using Flights.Data.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Flights.Data.Database;

public static class Configuration
{
    public static IServiceCollection RegisterDatabaseComponents(this IServiceCollection services)
    {
        MongoDbMappings.ConfigureMappings();

        var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        var client = new MongoClient(settings);
        var database = client.GetDatabase("flights");

        services.AddSingleton(database);

        services.RegisterRepositories();

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<IFlightQueryRepository, FlightQueryRepository>();
        services.AddTransient<IFlightQueryResultRepository, FlightQueryResultRepository>();
        services.AddTransient<IFlightQueryAlarmRepository, FlightQueryAlertRepository>();
        services.AddTransient<IFlightQueryNotificationRepository, FlightQueryNotificationRepository>();
        services.AddTransient<IAirportRepository, AirportRepository>();

        return services;
    }
}
