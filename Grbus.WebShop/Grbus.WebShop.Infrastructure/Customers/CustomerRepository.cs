using Grbus.WebShop.Domain.Aggregates.Customers;
using Grbus.WebShop.Domain.Aggregates.Customers.Events;
using Grbus.WebShop.Domain.Aggregates.Customers.Repository;
using Grbus.WebShop.Infrastructure.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Grbus.WebShop.Infrastructure.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) => _context = context;

        public async Task<Customer?> GetByIdAsync(string email)
        {
            return await _context.Set<Customer>().FindAsync(email);
        }

        public async Task InsertAsync(Customer customer)
        {            
            await _context.Set<Customer>().AddAsync(customer);
            customer.DomainEvents.Add(new CustomerCreatedEvent(customer));
        }

        public void UpdateAsync(Customer customer)
        {
            _context.Set<Customer>().Update(customer);
        }
    }
}
