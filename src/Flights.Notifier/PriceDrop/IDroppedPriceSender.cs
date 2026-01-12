using Flights.Data.Models.Notification;

namespace Flights.Notifier.PriceDrop;

public interface IDroppedPriceSender
{
    AlertTargetType Type { get; }
    Task NotifyDroppedPriceAsync(DroppedPrice droppedPrice, AlertTarget target);
}
