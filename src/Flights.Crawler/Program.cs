using Flights.Crawler;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection().RegisterComponents().BuildServiceProvider();
var flightCrawler = services.GetRequiredService<IFlightsCrawler>();

await flightCrawler.CrawlAsync();
