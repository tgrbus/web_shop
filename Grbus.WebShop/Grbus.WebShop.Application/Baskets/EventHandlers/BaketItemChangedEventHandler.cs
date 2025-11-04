using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Domain.Aggregates.Baskets.Events;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;
using MediatR;

namespace Grbus.WebShop.Application.Baskets.EventHandlers
{
    public class BaketItemChangedEventHandler : INotificationHandler<DomainEventNotification<BasketItemChangedEvent>>
    {
        private readonly IBasketRepository _basketRepository;

        public BaketItemChangedEventHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task Handle(DomainEventNotification<BasketItemChangedEvent> notification, CancellationToken ct)
        {
            var @event = notification.DomainEvent;
            await _basketRepository.AddHistoryAsync(@event);
        }
    }
}
