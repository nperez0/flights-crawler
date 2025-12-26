using Flights.Data.Models.Notification;

namespace Flights.Data.Database.Repositories;

public interface IFlightQueryAlarmRepository
{
    Task<FlightQueryPriceDropAlarm[]> GetAllPriceDropAlarmsAsync();
}