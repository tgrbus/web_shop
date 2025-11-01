namespace Grbus.WebShop.Domain.Aggregates.Baskets
{
    public class Basket
    {
        public required string CustomerEmail { get; set; }

        public List<BasketItem> BasketItems { get; private set; } = [];


        public void AddBasketItem(BasketItem item)
        {
            BasketItems.Add(item);
        }

        public void RemoveBasketItem(BasketItem item)
        {
            BasketItems.Remove(item);
        }

        public void AddBasketItemsList(List<BasketItem> items)
        {
            BasketItems.AddRange(items);
        }
    }
}
