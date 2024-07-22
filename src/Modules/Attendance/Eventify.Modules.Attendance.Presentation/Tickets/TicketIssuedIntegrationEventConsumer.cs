using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Attendance.Application.Tickets.CreateTicket;
using Eventify.Modules.Ticketing.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Attendance.Presentation.Tickets;

public sealed class TicketIssuedIntegrationEventConsumer(ISender sender)
    : IConsumer<TicketIssuedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<TicketIssuedIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new CreateTicketCommand(
                context.Message.TicketId,
                context.Message.CustomerId,
                context.Message.EventId,
                context.Message.Code),
            context.CancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateTicketCommand), result.Error);
        }
    }
}
