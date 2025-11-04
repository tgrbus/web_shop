using Grbus.WebShop.Domain.Aggregates.Baskets;
using Grbus.WebShop.Domain.Aggregates.Baskets.Events;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;
using Grbus.WebShop.Infrastructure.Common;

namespace Grbus.WebShop.Infrastructure.Baskets
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDbContext _context;

        public BasketRepository(ApplicationDbContext context)
        {
            _context = context;
        }        

        public async Task<Basket?> GetBasketByIdAsync(string email)
        {
            var basket = await _context.Set<Basket>().FindAsync(email);           
            return basket;
        }

        public async Task AddBasketAsync(Basket basket)
        {
            await _context.Set<Basket>().AddAsync(basket);
        }

        public void UpdateBasket(Basket basket)
        {
            _context.Set<Basket>().Update(basket);
        }

        public async Task AddHistoryAsync(BasketItemChangedEvent @event)
        {
            var history = new BasketHistory
            {
                Email = @event.BasketItem.BasketId,
                ProductId = @event.BasketItem.ProductId,
                Quantity = @event.BasketItem.Quantity,
                Timestamp = @event.OccurredOn
            };
            await _context.Set<BasketHistory>().AddAsync(history);
        }
    }
}
