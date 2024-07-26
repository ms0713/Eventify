using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Attendance.Application.Tickets.CreateTicket;
using Eventify.Modules.Ticketing.IntegrationEvents;
using MediatR;

namespace Eventify.Modules.Attendance.Presentation.Tickets;

public sealed class TicketIssuedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<TicketIssuedIntegrationEvent>
{
    public override async Task Handle(
        TicketIssuedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateTicketCommand(
                integrationEvent.TicketId,
                integrationEvent.CustomerId,
                integrationEvent.EventId,
                integrationEvent.Code),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateTicketCommand), result.Error);
        }
    }
}
