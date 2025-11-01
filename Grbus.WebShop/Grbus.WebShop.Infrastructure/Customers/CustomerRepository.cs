using Grbus.WebShop.Domain.Aggregates.Customers;
using Grbus.WebShop.Domain.Aggregates.Customers.Repository;

namespace Grbus.WebShop.Infrastructure.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<Customer> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Customer> GetQueriable()
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
