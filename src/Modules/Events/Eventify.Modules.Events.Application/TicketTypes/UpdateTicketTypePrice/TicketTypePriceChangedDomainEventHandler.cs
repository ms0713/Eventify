using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Messaging;
using Eventify.Modules.Events.Domain.TicketTypes;
using Eventify.Modules.Events.IntegrationEvents;

namespace Eventify.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
internal sealed class TicketTypePriceChangedDomainEventHandler(IEventBus eventBus)
     : DomainEventHandler<TicketTypePriceChangedDomainEvent>
{
    public override async Task Handle(
        TicketTypePriceChangedDomainEvent domainEvent, 
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new TicketTypePriceChangedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.TicketTypeId,
                domainEvent.Price),
            cancellationToken);
    }
}
