using Grbus.WebShop.Domain.Aggregates.Baskets;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;

namespace Grbus.WebShop.Infrastructure.Baskets
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDbContext _context;

        public BasketRepository(ApplicationDbContext context)
        {
            _context = context;
        }        

        public async Task<Basket?> GetBasketById(string email)
        {
            var basket = await _context.Set<Basket>().FindAsync(email);
            if (basket != null)
            {
                var basketItems = _context.Set<BasketItem>()
                        .Where(bi => bi.BasketId == email)
                        .Where(bi => bi.Active)
                        .Where(bi => bi.Quantity > 0)
                        .ToList();

                basket.AddBasketItemsList(basketItems);
            }
            return basket;
        }

        public async Task AddBasket(Basket basket)
        {
            await _context.Set<Basket>().AddAsync(basket);
        }

        public void UpdateBasket(Basket basket)
        {
            _context.Set<Basket>().Update(basket);
        }
    }
}
