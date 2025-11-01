namespace Grbus.WebShop.Domain.Common
{
    public interface IHasDomainEvent
    {
        List<DomainEvent> DomainEvents { get; set; }
    }
}
