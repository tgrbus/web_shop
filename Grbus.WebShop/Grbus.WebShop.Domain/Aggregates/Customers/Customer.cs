using Grbus.WebShop.Domain.Aggregates.Customers.Events;
using Grbus.WebShop.Domain.Common;

namespace Grbus.WebShop.Domain.Aggregates.Customers
{
    public class Customer : IHasDomainEvent
    {
        public Customer()
        {            
        }

        public required string Email { get; set; } // Primary key
        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        public List<DomainEvent> DomainEvents { get; set; } = [];
    }
}
