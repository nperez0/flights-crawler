using Flights.Data.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Ita.Form.FormFillers;

public interface IFormFillerFactory
{
    IFormFiller Create(IPage page, FlightQuery query);
}