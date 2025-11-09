using Grbus.WebShop.Domain.Common;

namespace Grbus.WebShop.Domain.Aggregates.Products
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string SKU { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal TaxPercentage { get; set; }
        public int StockQuantity { get; private set; } = 0;

        public Product(int stockQuantity)
        {
            StockQuantity = stockQuantity;
        }

        public Result ChangeStockQuantity(int quantity)
        {
            if (StockQuantity + quantity < 0)
            {
                return Result.Failure(DomainErrors.StockShortage);
            }

            StockQuantity += quantity;
            return Result.Success();           
        }

        public Result SetStockQuantity(int quantity)
        {
            StockQuantity = quantity;
            return Result.Success();
        }
    }
}
