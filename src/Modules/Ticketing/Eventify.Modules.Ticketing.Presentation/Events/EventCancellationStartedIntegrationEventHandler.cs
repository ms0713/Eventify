using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Events.IntegrationEvents;
using Eventify.Modules.Ticketing.Application.Events.CancelEvent;
using MediatR;

namespace Eventify.Modules.Ticketing.Presentation.Events;
internal sealed class EventCancellationStartedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<EventCancellationStartedIntegrationEvent>
{
    public override async Task Handle(
        EventCancellationStartedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CancelEventCommand(integrationEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CancelEventCommand), result.Error);
        }
    }
}
