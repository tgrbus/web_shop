using Grbus.WebShop.Domain.Aggregates.Baskets;
using Grbus.WebShop.Domain.Aggregates.Baskets.Events;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;

namespace Grbus.WebShop.Domain.UnitTests.Baskets
{
    public class FakeBasketRepository : IBasketRepository
    {
        public Task AddBasketAsync(Basket basket)
        {
            throw new NotImplementedException();
        }

        public Task AddHistoryAsync(BasketItemChangedEvent @event)
        {
            throw new NotImplementedException();
        }

        public Task<Basket?> GetBasketByIdAsync(string email)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BasketHistory> GetHistory(string email)
        {
            throw new NotImplementedException();
        }

        public void UpdateBasket(Basket basket)
        {
            throw new NotImplementedException();
        }
    }
}
