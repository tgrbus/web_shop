using Autofac;
using MediatR;

namespace Grbus.WebShop.Application.Common
{
    public class DependencyInjection : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
            builder.RegisterAssemblyTypes(typeof(ApplicationLayer).Assembly)
                .Where(n => n.IsClosedTypeOf(typeof(IRequestHandler<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ApplicationLayer).Assembly)
                .Where(n => n.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ApplicationLayer).Assembly)
                .Where(n => n.IsClosedTypeOf(typeof(INotificationHandler<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
