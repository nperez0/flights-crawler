namespace Flights.Cleaner.Job;

public class CleanerJob(IEnumerable<ICleaner> cleaners) : ICleanerJob
{
    public async Task ExecuteAsync()
    {
        foreach (var cleaner in cleaners)
            await cleaner.CleanAsync();
    }
}
