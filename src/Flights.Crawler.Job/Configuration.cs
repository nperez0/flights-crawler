using Flights.Crawler.Ita;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Crawler.Job;

public static class Configuration
{
    public static IServiceCollection RegisterComponents(this IServiceCollection services)
    {
        services.AddSingleton<IFlightsCrawlRunner, FlightsCrawlRunner>();
        services.AddSingleton<FlightsCrawlerJob>();

        services.RegisterItaCrawlerComponents();

        return services;
    }
}
