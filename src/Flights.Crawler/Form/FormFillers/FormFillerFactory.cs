using Flights.Data.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Form.FormFillers;

public class FormFillerFactory : IFormFillerFactory
{
    public IFormFiller Create(IPage page, FlightQuery query)
    {
        return query.Type switch
        {
            FlightQueryType.OneWay => new OneWayFormFiller(page, query),
            FlightQueryType.RoundTrip => new RoundTripFormFiller(page, query),
            FlightQueryType.MultiCity => new MultiCityFormFiller(page, query),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
