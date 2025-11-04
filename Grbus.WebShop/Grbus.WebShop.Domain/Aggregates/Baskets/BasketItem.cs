namespace Grbus.WebShop.Domain.Aggregates.Baskets
{
    public class BasketItem
    {
        public int Id { get; set; }
        public required string BasketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
