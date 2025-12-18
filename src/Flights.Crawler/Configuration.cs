using Flights.Crawler.Database;
using Flights.Crawler.Database.Repositories;
using Flights.Crawler.Form;
using Flights.Crawler.Form.FormFillers;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Flights.Crawler;

public static class Configuration
{
    public static IServiceCollection RegisterComponents(this IServiceCollection services)
    {
        services.RegisterDatabaseComponents();
        services.RegisterRepositories();
        services.RegisterCrawlerComponents();

        return services;
    }

    private static IServiceCollection RegisterDatabaseComponents(this IServiceCollection services)
    {
        MongoDbMappings.ConfigureMappings();

        var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        var client = new MongoClient(settings);

        services.AddSingleton<IMongoClient>(client);

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<IFlightQueryRepository, FlightQueryRepository>();
        services.AddTransient<IFlightQueryResultRepository, FlightQueryResultRepository>();

        return services;
    }

    private static IServiceCollection RegisterCrawlerComponents(this IServiceCollection services)
    {
        services.AddSingleton<IFlightsCrawler, FlightsCrawler>();

        services.AddSingleton<IPlaywrightPageFactory, PlaywrightPageFactory>();
        services.AddSingleton<IQueryResponseInterceptorFactory, QueryResponseInterceptorFactory>();
        services.AddSingleton<IFormFillerFactory, FormFillerFactory>();

        return services;
    }
}
