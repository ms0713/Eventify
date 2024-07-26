using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Events.IntegrationEvents;
using Eventify.Modules.Ticketing.Application.TicketTypes.UpdateTicketTypePrice;
using MediatR;

namespace Eventify.Modules.Ticketing.Presentation.TicketTypes;

public sealed class TicketTypePriceChangedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<TicketTypePriceChangedIntegrationEvent>
{
    public override async Task Handle(
        TicketTypePriceChangedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateTicketTypePriceCommand(integrationEvent.TicketTypeId, integrationEvent.Price),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(UpdateTicketTypePriceCommand), result.Error);
        }
    }
}
