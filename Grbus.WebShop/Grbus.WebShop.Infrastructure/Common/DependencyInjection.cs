using Autofac;
using Grbus.WebShop.Application.Common;
using Grbus.WebShop.Domain.Aggregates.Baskets.Repository;
using Grbus.WebShop.Domain.Aggregates.Customers.Repository;
using Grbus.WebShop.Domain.Aggregates.Products;
using Grbus.WebShop.Domain.Common;
using Grbus.WebShop.Infrastructure.Baskets;
using Grbus.WebShop.Infrastructure.Customers;
using Grbus.WebShop.Infrastructure.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Grbus.WebShop.Infrastructure.Common
{
    public class DependencyInjection : Module
    {
        private readonly IConfiguration _configuration;

        public DependencyInjection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(n => new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]).Options)
                .As<DbContextOptions<ApplicationDbContext>>()
                .InstancePerLifetimeScope();
            builder.RegisterType(typeof(ApplicationDbContext)).AsSelf().As(typeof(DbContext)).InstancePerLifetimeScope();

            // Register repositories
            builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BasketRepository>().As<IBasketRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();

            //
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            
            //
            builder.RegisterType<DomainEventService>().As<IDomainEventService>().InstancePerLifetimeScope();

            // 
            builder.RegisterType<MemoryCachingService>().As<ICachingService>().InstancePerLifetimeScope();

        }
    }
}
