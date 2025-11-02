using Grbus.WebShop.Application.Products.DTOs;

namespace Grbus.WebShop.Application.Baskets.DTOs
{
    public record BasketItemDto
    {
        public int Id { get; init; }
        public int ProductId { get; init; }
        public int Quantity { get; init; }
        public required string ProductName { get; init; }
        public decimal ProductPrice { get; init; }
        public decimal TotalPrice => ProductPrice * Quantity;

        public required ProductDto Product { get; init; }
    }
}
