using Flights.Cleaner.Job;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection().RegisterComponents().BuildServiceProvider();
var job = services.GetRequiredService<ICleanerJob>();

await job.ExecuteAsync();