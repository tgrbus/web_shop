using Grbus.WebShop.Domain.Aggregates.Baskets.Events;
using Grbus.WebShop.Domain.Common;

namespace Grbus.WebShop.Domain.Aggregates.Baskets.Repository
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketByIdAsync(string email);

        Task AddBasketAsync(Basket basket);

        void UpdateBasket(Basket basket);

        Task AddHistoryAsync(BasketItemChangedEvent @event);
    }
}
