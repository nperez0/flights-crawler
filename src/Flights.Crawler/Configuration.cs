using Flights.Crawler.Form;
using Flights.Crawler.Form.FormFillers;
using Flights.Data.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Crawler;

public static class Configuration
{
    public static IServiceCollection RegisterComponents(this IServiceCollection services)
    {
        services.RegisterDatabaseComponents();
        services.RegisterCrawlerComponents();

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
