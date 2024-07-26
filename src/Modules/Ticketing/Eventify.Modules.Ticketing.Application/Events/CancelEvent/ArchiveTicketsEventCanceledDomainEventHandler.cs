using Eventify.Common.Application.Exceptions;
using Eventify.Common.Application.Messaging;
using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Application.Tickets.ArchiveTicketsForEvent;
using Eventify.Modules.Ticketing.Domain.Events;
using MediatR;

namespace Eventify.Modules.Ticketing.Application.Events.CancelEvent;
internal sealed class ArchiveTicketsEventCanceledDomainEventHandler(ISender sender)
    : DomainEventHandler<EventCanceledDomainEvent>
{
    public override async Task Handle(
        EventCanceledDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new ArchiveTicketsForEventCommand(domainEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(ArchiveTicketsForEventCommand), result.Error);
        }
    }
}
