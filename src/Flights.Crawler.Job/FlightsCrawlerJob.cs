namespace Flights.Crawler.Job;

public  class FlightsCrawlerJob(IFlightsCrawlRunner flightsCrawlRunner)
{
    public async ValueTask ExecuteAsync()
    {
        await flightsCrawlRunner.CrawlAsync();
    }
}
