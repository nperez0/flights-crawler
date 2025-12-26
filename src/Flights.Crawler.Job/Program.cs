using Flights.Crawler.Job;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var services = new ServiceCollection().RegisterComponents().BuildServiceProvider();
var job = services.GetRequiredService<FlightsCrawlerJob>();

await job.ExecuteAsync();