namespace Grbus.WebShop.Application.Products.DTOs
{
    public record ProductDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string SKU { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public decimal TaxPercentage { get; init; }
        public int? StockQuantity { get; init; }
    }
}
