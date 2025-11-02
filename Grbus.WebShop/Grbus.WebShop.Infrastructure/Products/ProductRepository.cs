using Grbus.WebShop.Domain.Aggregates.Products;
using Grbus.WebShop.Infrastructure.Common;

namespace Grbus.WebShop.Infrastructure.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Set<Product>().FindAsync(id);
        }

        public IQueryable<Product> GetQueriable()
        {
            return _context.Set<Product>().AsQueryable();
        }

        public async Task InsertAsync(Product product)
        {
            await _context.Set<Product>().AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Set<Product>().Update(product);
        }
    }
}
