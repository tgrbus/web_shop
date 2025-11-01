namespace Grbus.WebShop.Domain.Aggregates.Customers.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync();
        IQueryable<Customer> GetQueriable();
        Task InsertAsync(Customer customer);
        Task UpdateAsync(Customer customer);
    }
}
