namespace Grbus.WebShop.Domain.Aggregates.Baskets.Repository
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketById(string email);

        Task AddBasket(Basket basket);

        void UpdateBasket(Basket basket);
    }
}
