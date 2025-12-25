
namespace Flights.Cleaner.Job;

public interface ICleanerJob
{
    Task ExecuteAsync();
}