using Grbus.WebShop.Domain.Common;

namespace Grbus.WebShop.Domain.Aggregates.Baskets.Events
{
    public record BasketItemChangedEvent : DomainEvent
    {
        public BasketItem BasketItem { get; init; }

        public BasketItemChangedEvent(BasketItem basketItem) => BasketItem = basketItem;
    }
}
