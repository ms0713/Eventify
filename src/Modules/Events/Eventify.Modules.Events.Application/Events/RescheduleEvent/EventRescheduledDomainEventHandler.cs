using Eventify.Common.Application.Messaging;
using Eventify.Modules.Events.Domain.Events;

namespace Eventify.Modules.Events.Application.Events.RescheduleEvent;
internal sealed class EventRescheduledDomainEventHandler : IDomainEventHandler<EventRescheduledDomainEvent>
{
    public Task Handle(EventRescheduledDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
