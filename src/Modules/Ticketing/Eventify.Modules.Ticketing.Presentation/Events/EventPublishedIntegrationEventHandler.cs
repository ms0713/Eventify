using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Events.IntegrationEvents;
using Eventify.Modules.Ticketing.Application.Events.CreateEvent;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Ticketing.Presentation.Events;

public sealed class EventPublishedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<EventPublishedIntegrationEvent>
{
    public override async Task Handle(
        EventPublishedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateEventCommand(
                integrationEvent.EventId,
                integrationEvent.Title,
                integrationEvent.Description,
                integrationEvent.Location,
                integrationEvent.StartsAtUtc,
                integrationEvent.EndsAtUtc,
                integrationEvent.TicketTypes
                    .Select(t => new CreateEventCommand.TicketTypeRequest(
                        t.Id,
                        integrationEvent.EventId,
                        t.Name,
                        t.Price,
                        t.Currency,
                        t.Quantity))
                    .ToList()),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateEventCommand), result.Error);
        }
    }
}
