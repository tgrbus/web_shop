using Grbus.WebShop.Domain.Aggregates.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Grbus.WebShop.Infrastructure.Products
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.StockQuantity).IsRequired();           
            
            
            // Add initial data
            builder.HasData(
                new Product
                {
                    Id = 1,
                    SKU = "SKU001",
                    Name = "Sample Product 01",
                    Description = "This is a sample product.",
                    Price = 19.99m,
                    StockQuantity = 100
                },
                new Product
                {
                    Id = 2,
                    SKU = "SKU002",
                    Name = "Sample Product 02",
                    Description = "This is another sample product.",
                    Price = 29.99m,
                    StockQuantity = 50
                });
        }
    }
}
