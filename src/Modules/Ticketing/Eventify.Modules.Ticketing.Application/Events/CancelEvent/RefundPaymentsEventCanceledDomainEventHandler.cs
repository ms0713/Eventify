using Eventify.Common.Application.Exceptions;
using Eventify.Common.Application.Messaging;
using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Application.Payments.RefundPaymentsForEvent;
using Eventify.Modules.Ticketing.Domain.Events;
using MediatR;

namespace Eventify.Modules.Ticketing.Application.Events.CancelEvent;

internal sealed class RefundPaymentsEventCanceledDomainEventHandler(ISender sender)
    : IDomainEventHandler<EventCanceledDomainEvent>
{
    public async Task Handle(EventCanceledDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        Result result = await sender.Send(new RefundPaymentsForEventCommand(domainEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(RefundPaymentsForEventCommand), result.Error);
        }
    }
}
