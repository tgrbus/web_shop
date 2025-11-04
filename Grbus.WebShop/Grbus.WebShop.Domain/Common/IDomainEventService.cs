namespace Grbus.WebShop.Domain.Common
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
