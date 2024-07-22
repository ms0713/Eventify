using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Messaging;
using Eventify.Modules.Ticketing.Domain.Events;
using Eventify.Modules.Ticketing.IntegrationEvents;

namespace Eventify.Modules.Ticketing.Application.TicketTypes;

internal sealed class TicketTypeSoldOutDomainEventHandler(IEventBus eventBus)
    : IDomainEventHandler<TicketTypeSoldOutDomainEvent>
{
    public async Task Handle(TicketTypeSoldOutDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(
            new TicketTypeSoldOutIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.TicketTypeId),
            cancellationToken);
    }
}
