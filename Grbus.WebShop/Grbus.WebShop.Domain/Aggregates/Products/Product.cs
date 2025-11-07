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
        public int StockQuantity { get; set; }

        //public Result ChangeQuantity(int quantity)
        //{
        //    if(StockQuantity + quantity < 0)
        //    {
        //        return Result.Failure();
        //    }

        //    StockQuantity += quantity;

        //    if(S)
        //}
    }
}
