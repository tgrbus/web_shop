using Grbus.WebShop.Domain.Aggregates.Customers.Events;
using Grbus.WebShop.Domain.Common;

namespace Grbus.WebShop.Domain.Aggregates.Customers
{
    public class Customer : IHasDomainEvent
    {
        public Customer()
        {
            DomainEvents.Add(new CustomerCreatedEvent { Customer = this });
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = [];
    }
}
