using Grbus.WebShop.Domain.Aggregates.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Grbus.WebShop.Infrastructure.Customers
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FirstName).HasMaxLength(100);
            builder.Property(c => c.LastName).HasMaxLength(100);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
        }
    }
}
