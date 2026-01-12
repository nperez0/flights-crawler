using Flights.Notifier.Job;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection()
    .RegisterComponents()
    .BuildServiceProvider();

var job = services.GetRequiredService<IFlightsNotifierJob>();

await job.ExecuteAsync();
