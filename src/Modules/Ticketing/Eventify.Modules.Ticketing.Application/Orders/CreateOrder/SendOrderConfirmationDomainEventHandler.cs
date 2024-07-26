using Eventify.Common.Application.Exceptions;
using Eventify.Common.Application.Messaging;
using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Application.Orders.GetOrder;
using Eventify.Modules.Ticketing.Domain.Orders;
using MediatR;

namespace Eventify.Modules.Ticketing.Application.Orders.CreateOrder;

internal sealed class SendOrderConfirmationDomainEventHandler(ISender sender)
    : DomainEventHandler<OrderCreatedDomainEvent>
{
    public override async Task Handle(
        OrderCreatedDomainEvent notification,
        CancellationToken cancellationToken = default)
    {
        Result<OrderResponse> result = await sender.Send(new GetOrderQuery(notification.OrderId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(GetOrderQuery), result.Error);
        }

        // Send order confirmation notification.
    }
}
