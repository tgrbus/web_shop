using Grbus.WebShop.Domain.Aggregates.Baskets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Grbus.WebShop.Infrastructure.Baskets
{
    internal class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.Property(bi => bi.Quantity).IsRequired();
            builder.Property(bi => bi.Active).IsRequired();
            builder.Property(bi => bi.CreatedAt).IsRequired();
            builder.Property(bi => bi.DeactivatedAt);
        }
    }
}
