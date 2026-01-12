using Flights.Data.Models.Notification;

namespace Flights.Data.Database.Repositories;

public interface IFlightQueryAlertRepository
{
    Task<FlightQueryPriceDropAlert[]> GetAllPriceDropAlertsAsync();
}