using Flights.Crawler.Ita.Form;
using Flights.Crawler.Ita.Form.FormFillers;
using Microsoft.Extensions.DependencyInjection;

namespace Flights.Crawler.Ita;

public static class Configuration
{
    public static IServiceCollection RegisterItaCrawlerComponents(this IServiceCollection services)
    {
        services.AddSingleton<IFlightsCrawler, ItaFlightsCrawler>();
        services.AddSingleton<IPlaywrightPageFactory, PlaywrightPageFactory>();
        services.AddSingleton<IQueryResponseInterceptorFactory, QueryResponseInterceptorFactory>();
        services.AddSingleton<IFormFillerFactory, FormFillerFactory>();

        return services;
    }
}
