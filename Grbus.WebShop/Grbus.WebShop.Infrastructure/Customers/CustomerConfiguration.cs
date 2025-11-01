using Grbus.WebShop.Domain.Aggregates.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Grbus.WebShop.Infrastructure.Customers
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(c => c.Email);

            builder.Property(c => c.FirstName).HasMaxLength(100);
            builder.Property(c => c.LastName).HasMaxLength(100);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(255);


            // Add initial data
            builder.HasData(
                new Customer
                {
                    Email = "tomislav.grbus@gmail.com",
                    FirstName = "Tomislav",
                    LastName = "Grbus"
                });
        }
    }
}
