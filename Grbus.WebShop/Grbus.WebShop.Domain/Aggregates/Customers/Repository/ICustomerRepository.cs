namespace Grbus.WebShop.Domain.Aggregates.Customers.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(string email);
        Task InsertAsync(Customer customer);
        void UpdateAsync(Customer customer);
    }
}
