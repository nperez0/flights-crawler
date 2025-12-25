// See https://aka.ms/new-console-template for more information

using Flights.Notifier.Job;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection()
    .RegisterComponents(configuration)
    .BuildServiceProvider();

var job = services.GetRequiredService<IFlightsNotifierJob>();

await job.ExecuteAsync();
