using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Events.IntegrationEvents;
using Eventify.Modules.Ticketing.Application.TicketTypes.UpdateTicketTypePrice;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Ticketing.Presentation.TicketTypes;

public sealed class TicketTypePriceChangedIntegrationEventConsumer(ISender sender)
    : IConsumer<TicketTypePriceChangedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<TicketTypePriceChangedIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new UpdateTicketTypePriceCommand(context.Message.TicketTypeId, context.Message.Price),
            context.CancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(UpdateTicketTypePriceCommand), result.Error);
        }
    }
}
