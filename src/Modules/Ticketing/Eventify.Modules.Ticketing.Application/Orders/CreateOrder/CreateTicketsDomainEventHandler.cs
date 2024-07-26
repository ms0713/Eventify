using Eventify.Common.Application.Exceptions;
using Eventify.Common.Application.Messaging;
using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Application.Tickets.CreateTicketBatch;
using Eventify.Modules.Ticketing.Domain.Orders;
using MediatR;

namespace Eventify.Modules.Ticketing.Application.Orders.CreateOrder;

internal sealed class CreateTicketsDomainEventHandler(ISender sender)
    : DomainEventHandler<OrderCreatedDomainEvent>
{
    public override async Task Handle(
        OrderCreatedDomainEvent notification,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CreateTicketBatchCommand(notification.OrderId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateTicketBatchCommand), result.Error);
        }
    }
}
