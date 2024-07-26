using Eventify.Common.Application.Exceptions;
using Eventify.Common.Application.Messaging;
using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Application.Tickets.GetTicket;
using Eventify.Modules.Ticketing.Application.Tickets.GetTicketForOrder;
using Eventify.Modules.Ticketing.Domain.Orders;
using MediatR;

namespace Eventify.Modules.Ticketing.Application.Tickets.CreateTicketBatch;
internal sealed class OrderTicketsIssuedDomainEventHandler(ISender sender)
    : DomainEventHandler<OrderTicketsIssuedDomainEvent>
{
    public override async Task Handle(
    OrderTicketsIssuedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<IReadOnlyCollection<TicketResponse>> result = await sender.Send(
            new GetTicketsForOrderQuery(domainEvent.OrderId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(GetTicketsForOrderQuery), result.Error);
        }

        // Send ticket confirmation notification.
    }
}
