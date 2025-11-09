using Grbus.WebShop.Domain.Aggregates.Customers;
using Grbus.WebShop.Domain.Aggregates.Customers.Repository;

namespace Grbus.WebShop.Domain.UnitTests.Customers
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Task<Customer?> GetByIdAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
