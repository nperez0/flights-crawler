using Flights.Data.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Form.FormFillers;

public class FormFillerFactory : IFormFillerFactory
{
    public IFormFiller Create(IPage page, FlightQuery search)
    {
        return search.Type switch
        {
            FlightQueryType.OneWay => new OneWayFormFiller(page, search),
            FlightQueryType.RoundTrip => new RoundTripFormFiller(page, search),
            FlightQueryType.MultiCity => new MultiCityFormFiller(page, search),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
