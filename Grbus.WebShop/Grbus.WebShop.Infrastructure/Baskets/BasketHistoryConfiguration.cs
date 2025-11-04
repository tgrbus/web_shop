using Grbus.WebShop.Domain.Aggregates.Baskets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Grbus.WebShop.Infrastructure.Baskets
{
    internal class BasketHistoryConfiguration : IEntityTypeConfiguration<BasketHistory>
    {
        public void Configure(EntityTypeBuilder<BasketHistory> builder)
        {
            builder.ToTable("BasketHistory");

            builder.HasKey(x => x.Id);

            builder.Property(n => n.Email).IsRequired().HasMaxLength(255);
            builder.Property(n => n.ProductId).IsRequired();
            builder.Property(n => n.Quantity).IsRequired();
            builder.Property(n => n.Timestamp).IsRequired();

            builder.HasOne(n => n.Basket).WithMany().HasForeignKey(n => n.Email);
            builder.HasOne(n => n.Product).WithMany().HasForeignKey(p => p.ProductId);
        }
    }
}
