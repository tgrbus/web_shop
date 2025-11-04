using Grbus.WebShop.Domain.Aggregates.Products;

namespace Grbus.WebShop.Domain.Aggregates.Baskets
{
    public class BasketHistory
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public virtual Basket Basket { get; set; }
        public virtual Product Product { get; set; }
    }
}
