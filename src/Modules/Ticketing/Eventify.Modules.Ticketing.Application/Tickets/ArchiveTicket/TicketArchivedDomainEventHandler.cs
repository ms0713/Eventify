using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Messaging;
using Eventify.Modules.Ticketing.Domain.Tickets;
using Eventify.Modules.Ticketing.IntegrationEvents;

namespace Eventify.Modules.Ticketing.Application.Tickets.ArchiveTicket;

internal sealed class TicketArchivedDomainEventHandler(IEventBus eventBus)
    : IDomainEventHandler<TicketArchivedDomainEvent>
{
    public async Task Handle(TicketArchivedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(
            new TicketArchivedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.TicketId,
                domainEvent.Code),
            cancellationToken);
    }
}
