namespace Grbus.WebShop.Domain.Aggregates.Products
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task InsertAsync(Product product);
        void Update(Product product);
    }
}
