using Flights.Data.Database.Repositories;

namespace Flights.Crawler;

public class FlightsCrawlRunner(
    IEnumerable<IFlightsCrawler> crawlers,
    IFlightQueryRepository flightQueryRepository,
    IFlightQueryResultRepository flightQueryResultRepository) 
    : IFlightsCrawlRunner
{
    private readonly IFlightsCrawler[] crawlers = [.. crawlers];

    public async Task CrawlAsync()
    {
        var queries = await flightQueryRepository.GetEnabledQueriesAsync();

        foreach (var crawler in crawlers)
        {
            var results = await crawler.ExecuteQueriesAsync(queries);

            await flightQueryResultRepository.SaveAsync(results);
        }
    }
}
