﻿using Eventify.Common.Application.Messaging;
using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Application.Abstractions.Data;
using Eventify.Modules.Ticketing.Domain.Events;
using Eventify.Modules.Ticketing.Domain.Orders;
using Eventify.Modules.Ticketing.Domain.Tickets;

namespace Eventify.Modules.Ticketing.Application.Tickets.CreateTicketBatch;

internal sealed class CreateTicketBatchCommandHandler(
    IOrderRepository orderRepository,
    ITicketTypeRepository ticketTypeRepository,
    ITicketRepository ticketRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateTicketBatchCommand>
{
    public async Task<Result> Handle(CreateTicketBatchCommand request, CancellationToken cancellationToken)
    {
        Order? order = await orderRepository.GetAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderId));
        }

        Result result = order.IssueTickets();

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        List<Ticket> tickets = [];
        foreach (OrderItem orderItem in order.OrderItems)
        {
            TicketType? ticketType = await ticketTypeRepository.GetAsync(orderItem.TicketTypeId, cancellationToken);

            if (ticketType is null)
            {
                return Result.Failure(TicketTypeErrors.NotFound(orderItem.TicketTypeId));
            }

            for (int i = 0; i < orderItem.Quantity; i++)
            {
                var ticket = Ticket.Create(order, ticketType);

                tickets.Add(ticket);
            }
        }

        ticketRepository.InsertRange(tickets);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
