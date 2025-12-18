using MongoDB.Driver;

namespace Flights.Data.Database.Repositories;

public static class IFindFluentExtensions
{
    public static async Task<TProjection[]> ToArrayAsync<TDocument, TProjection>(this IFindFluent<TDocument, TProjection> findFluent)
    {
        var result = await findFluent.ToListAsync();

        return [.. result];
    }
}
