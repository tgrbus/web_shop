using Grbus.WebShop.Domain.Aggregates.Baskets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Grbus.WebShop.Infrastructure.Baskets
{
    internal class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable("Basket");

            builder.HasKey(x => x.CustomerEmail);
            builder.Property(x => x.CustomerEmail).HasMaxLength(255); 

            builder.HasMany(x => x.BasketItems).WithOne().HasForeignKey(s => s.BasketId);


            // Add initial data
            builder.HasData(
                new Basket
                {
                    CustomerEmail = "tomislav.grbus@gmail.com"
                });
        }
    }
}
