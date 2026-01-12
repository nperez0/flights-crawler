namespace Flights.Crawler.Job;

public  class FlightsCrawlerJob(IFlightsCrawlRunner flightsCrawlRunner)
{
    public async Task ExecuteAsync()
    {
        await flightsCrawlRunner.CrawlAsync();
    }
}
