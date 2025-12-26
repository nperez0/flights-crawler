using Flights.Data.Models.Notification;

namespace Flights.Notifier.PriceDrop;

public interface IDroppedPriceSender
{
    AlarmTargetType Type { get; }
    Task NotifyDroppedPriceAsync(DroppedPrice droppedPrice, AlarmTarget target);
}
