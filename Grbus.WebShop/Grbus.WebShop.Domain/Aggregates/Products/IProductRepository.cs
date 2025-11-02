namespace Grbus.WebShop.Domain.Aggregates.Products
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task InsertAsync(Product product);
        IQueryable<Product> GetQueriable();
        void Update(Product product);
    }
}
