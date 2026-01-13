namespace Flights.Core.Extensions;

public static class IEnumerableExtensions
{
    public static void Each<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
            action(item);
    }

    public static async Task<IEnumerable<TResult>> SelectAsync<T, TResult>(this IEnumerable<T> source, Func<T, Task<TResult>> action)
    {
        var results = new List<TResult>();

        foreach (var item in source)
            results.Add(await action(item));

        return results;
    }
}
