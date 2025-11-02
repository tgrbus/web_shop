namespace Grbus.WebShop.Application.Baskets.DTOs
{
    public record BasketDto
    {
        public required string CustomerEmail { get; init; }

        public List<BasketItemDto> BasketItems { get; init; } = [];
    }
}
