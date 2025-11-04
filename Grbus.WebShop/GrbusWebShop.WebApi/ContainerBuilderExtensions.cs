using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;

namespace GrbusWebShop.WebApi
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterConfiguration(this ContainerBuilder contanerBuilder, IConfiguration configuration)
        {
            contanerBuilder.RegisterInstance(configuration).As<IConfiguration>();
            return contanerBuilder;
        }

        public static ContainerBuilder ConfigureContainer(this ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            var mediatRConfiguration = MediatRConfigurationBuilder
                    .Create(typeof(Program).Assembly)
                    .WithAllOpenGenericHandlerTypesRegistered()
                    .Build();

            containerBuilder.RegisterMediatR(mediatRConfiguration);

            containerBuilder.RegisterModule(new Grbus.WebShop.Infrastructure.Common.DependencyInjection(configuration));
            containerBuilder.RegisterModule(new Grbus.WebShop.Application.Common.DependencyInjection());

            return containerBuilder;
        }
    }
}
