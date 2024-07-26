using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Messaging;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.IntegrationEvents;

namespace Eventify.Modules.Events.Application.Events.RescheduleEvent;
internal sealed class EventRescheduledDomainEventHandler(IEventBus eventBus) 
    : DomainEventHandler<EventRescheduledDomainEvent>
{
    public override async Task Handle(
        EventRescheduledDomainEvent domainEvent, 
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new EventRescheduledIntegrationEvent(
            domainEvent.Id,
            domainEvent.OccurredOnUtc,
            domainEvent.EventId,
            domainEvent.StartsAtUtc,
            domainEvent.EndsAtUtc),
        cancellationToken);
    }
}
