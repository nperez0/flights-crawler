using Flights.Data.Database.Repositories;

namespace Flights.Cleaner.Job.Cleaners;

public class OldQueryResultsCleaner(IFlightQueryResultRepository flightQueryResultRepository) : ICleaner
{
    private const int ResultsAgeInDays = 30;

    public async Task CleanAsync()
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-ResultsAgeInDays);

        var oldResults = await flightQueryResultRepository.GetResultsOlderThanAsync(cutoffDate);
        
        await flightQueryResultRepository.DeleteResultsAsync(oldResults);
    }
}
