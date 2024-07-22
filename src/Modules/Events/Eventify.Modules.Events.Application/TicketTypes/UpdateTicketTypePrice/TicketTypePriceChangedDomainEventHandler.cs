using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Messaging;
using Eventify.Modules.Events.Domain.TicketTypes;
using Eventify.Modules.Events.IntegrationEvents;

namespace Eventify.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
internal sealed class TicketTypePriceChangedDomainEventHandler(IEventBus eventBus)
     : IDomainEventHandler<TicketTypePriceChangedDomainEvent>
{
    public async Task Handle(TicketTypePriceChangedDomainEvent domainEvent, CancellationToken cancellationToken)
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
