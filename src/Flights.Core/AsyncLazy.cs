namespace Flights.Core;

public class AsyncLazy<T>(Func<Task<T>> taskFactory) : Lazy<Task<T>>(taskFactory)
{
}

