using Grbus.WebShop.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Grbus.WebShop.Infrastructure.Common
{
    public class DomainEventService : IDomainEventService
    {
        private readonly IPublisher _publisher;
        private readonly ILogger<DomainEventService> _logger;

        public DomainEventService(IPublisher publisher, ILogger<DomainEventService> logger)
        {
            _publisher = publisher;
            _logger = logger;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            _logger.LogInformation("Publishing domain event of type {DomainEventType} occurred on {OccurredOn}", domainEvent.GetType().Name, domainEvent.OccurredOn);
            await _publisher.Publish(domainEvent);
        }
    }
}
