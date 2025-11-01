using Grbus.WebShop.Domain.Aggregates.Baskets;
using Grbus.WebShop.Domain.Aggregates.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Grbus.WebShop.Infrastructure.Baskets
{
    internal class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.ToTable("BasketItem");

            builder.HasKey(bi => bi.Id);

            builder.Property(bi => bi.BasketId).IsRequired();
            builder.Property(bi => bi.Quantity).IsRequired();
            builder.Property(bi => bi.ProductId).IsRequired();
            builder.Property(bi => bi.Active).IsRequired();
            builder.Property(bi => bi.CreatedAt).IsRequired();
            builder.Property(bi => bi.DeactivatedAt);

            builder.HasOne<Product>()
                   .WithMany()
                   .HasForeignKey(bi => bi.ProductId)
                   .IsRequired();
            builder.HasOne<Basket>()
                   .WithMany(b => b.BasketItems)
                   .HasForeignKey(bi => bi.BasketId)
                   .IsRequired();
        }
    }
}
