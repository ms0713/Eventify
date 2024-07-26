using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Attendance.Application.Events.CreateEvent;
using Eventify.Modules.Events.IntegrationEvents;
using MediatR;

namespace Eventify.Modules.Attendance.Presentation.Events;

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
                integrationEvent.EndsAtUtc),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateEventCommand), result.Error);
        }
    }
}
