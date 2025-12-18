using Flights.Crawler.Models;

namespace Flights.Crawler;

public interface IFlightsCrawler
{
    Task CrawlAsync();
}