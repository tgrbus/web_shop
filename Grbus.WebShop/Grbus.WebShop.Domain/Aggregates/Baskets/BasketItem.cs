namespace Grbus.WebShop.Domain.Aggregates.Baskets
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeactivatedAt { get; set; }
    }
}
