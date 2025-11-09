using Grbus.WebShop.Domain.Aggregates.Products;

namespace Grbus.WebShop.Domain.UnitTests.Products
{
    public class FakeProductRepository : IProductRepository
    {
        public Task<Product?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetQueriable()
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
