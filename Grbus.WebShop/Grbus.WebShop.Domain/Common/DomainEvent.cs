namespace Grbus.WebShop.Domain.Common
{
    public abstract record DomainEvent
    {
        public bool IsPublished { get; set; } = false;
        public DateTimeOffset OccurredOn { get; set; } = DateTimeOffset.UtcNow;
    }
}
