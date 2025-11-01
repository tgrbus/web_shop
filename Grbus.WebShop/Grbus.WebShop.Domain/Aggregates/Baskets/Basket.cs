namespace Grbus.WebShop.Domain.Aggregates.Baskets
{
    public class Basket
    {
        public int CustomerId { get; set; }

        public void AddItemToBasket(int productId, int quantity)
        {
            var item = new BasketItem
            {
                ProductId = productId,
                Quantity = quantity,
                Active = true,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
