using Grbus.WebShop.Domain.Aggregates.Products;

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
