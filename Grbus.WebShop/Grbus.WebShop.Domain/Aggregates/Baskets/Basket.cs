using Grbus.WebShop.Domain.Aggregates.Baskets.Events;
using Grbus.WebShop.Domain.Common;

namespace Grbus.WebShop.Domain.Aggregates.Baskets
{
    public class Basket : IHasDomainEvent
    {
        public required string CustomerEmail { get; set; }

        public List<BasketItem> BasketItems { get; private set; } = [];

        public List<DomainEvent> DomainEvents { get; set; } = [];

        public void AddProduct(int productId, int quantity)
        {
            if(quantity < 0)
            {
                return;
            }
            
            var existing = BasketItems.SingleOrDefault(n => n.ProductId == productId);
            if (existing != null)
            {
                IncreaseOrDecreaseQuantity(productId, quantity);
            }
            else
            {
                existing = new BasketItem
                {
                    BasketId = this.CustomerEmail,
                    ProductId = productId,
                    Quantity = quantity
                };
                BasketItems.Add(existing);
            }

            DomainEvents.Add(new BasketItemChangedEvent(existing));            
        }

        public void RemoveProduct(int productId)
        {
            if(BasketItems.Any(bi => bi.ProductId == productId))
            {
                var existingItem = BasketItems.Single(bi => bi.ProductId == productId);
                BasketItems.Remove(existingItem);
            }
        }

        public void IncreaseOrDecreaseQuantity(int productId, int quantity)
        {
            if(BasketItems.Any(bi => bi.ProductId == productId))
            {
                var existingItem = BasketItems.Single(bi => bi.ProductId == productId);
                existingItem.Quantity += quantity;

                if(existingItem.Quantity <= 0)
                {
                    RemoveProduct(productId);
                }
            }            
        }
    }
}
