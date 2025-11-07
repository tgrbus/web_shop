namespace Grbus.WebShop.Application.Baskets.DTOs
{
    public record BasketHistoryDto
    {
        public int Id { get; init; }
        public DateTimeOffset Timestamp { get; init; }
        public int ProductId { get; init; }
        public string? ProductName { get; init; }        
        public int Quantity { get; init; }
    }
}
