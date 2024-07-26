using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Exceptions;
using Eventify.Common.Application.Messaging;
using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Application.Tickets.GetTicket;
using Eventify.Modules.Ticketing.Domain.Tickets;
using Eventify.Modules.Ticketing.IntegrationEvents;
using MediatR;

namespace Eventify.Modules.Ticketing.Application.Tickets.CreateTicketBatch;

internal sealed class TicketCreatedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<TicketCreatedDomainEvent>
{
    public override async Task Handle(
        TicketCreatedDomainEvent domainEvent, 
        CancellationToken cancellationToken = default)
    {
        Result<TicketResponse> result = await sender.Send(
            new GetTicketQuery(domainEvent.TicketId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(GetTicketQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new TicketIssuedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.CustomerId,
                result.Value.EventId,
                result.Value.Code),
            cancellationToken);
    }
}
