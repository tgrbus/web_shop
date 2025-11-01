using Grbus.WebShop.Domain.Common;

namespace Grbus.WebShop.Domain.Aggregates.Customers.Events
{
    public record CustomerCreatedEvent : DomainEvent
    {
        public Customer Customer { get; init; }
    }
}
