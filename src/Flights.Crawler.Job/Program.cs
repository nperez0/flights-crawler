using Flights.Crawler.Job;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection().RegisterComponents().BuildServiceProvider();
var job = services.GetRequiredService<FlightsCrawlerJob>();

await job.ExecuteAsync();