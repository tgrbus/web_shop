namespace Grbus.WebShop.Domain.Aggregates.Baskets.Repository
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasketById(int id);
    }
}
