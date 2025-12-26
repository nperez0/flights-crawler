using Flights.Data.Database.Repositories;
using Microsoft.Extensions.Logging;

namespace Flights.Crawler;

public class FlightsCrawlRunner(
    IEnumerable<IFlightsCrawler> crawlers,
    IFlightQueryRepository flightQueryRepository,
    IFlightQueryResultRepository flightQueryResultRepository,
    ILogger<FlightsCrawlRunner> logger) 
    : IFlightsCrawlRunner
{
    private readonly IFlightsCrawler[] crawlers = [.. crawlers];

    public async Task CrawlAsync()
    {
        var queries = await flightQueryRepository.GetEnabledQueriesAsync();

        foreach (var crawler in crawlers)
        {
            var results = await crawler.ExecuteQueriesAsync(queries);

            logger.LogInformation("Crawler {Crawler} found {ResultCount} results.", 
                crawler.Name, results.Length);

            await flightQueryResultRepository.SaveAsync(results);
        }
    }
}
