using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Domain.Aggregates.Baskets;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;
using Grbus.WebShop.Domain.Aggregates.Customers.Events;
using MediatR;

namespace Grbus.WebShop.Application.Customers.EventHandlers
{
    public class CustomerCreatedEventHandler : INotificationHandler<DomainEventNotification<CustomerCreatedEvent>>
    {
        private readonly IBasketRepository _basketRepo;
        public CustomerCreatedEventHandler(IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
        }

        public async Task Handle(DomainEventNotification<CustomerCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var customerEvent = notification.DomainEvent;
            var basket = await _basketRepo.GetBasketByIdAsync(customerEvent.Customer.Email);
            if (basket == null)
            {
                await _basketRepo.AddBasketAsync(new Basket { CustomerEmail = customerEvent.Customer.Email });
            }
        }
    }
}
