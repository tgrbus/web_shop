namespace Grbus.WebShop.Application.Products.DTOs
{
    public record ProductDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }
}
