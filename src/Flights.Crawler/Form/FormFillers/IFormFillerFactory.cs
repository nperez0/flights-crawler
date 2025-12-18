using Flights.Data.Models.Query;
using Microsoft.Playwright;

namespace Flights.Crawler.Form.FormFillers;

public interface IFormFillerFactory
{
    IFormFiller Create(IPage page, FlightQuery search);
}