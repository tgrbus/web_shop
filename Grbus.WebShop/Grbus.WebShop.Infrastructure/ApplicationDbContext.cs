using Grbus.WebShop.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Grbus.WebShop.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.Ignore<DomainEvent>();
            base.OnModelCreating(modelBuilder);
        }        
    }
}
