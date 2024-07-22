﻿using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Events.IntegrationEvents;
using Eventify.Modules.Ticketing.Application.Events.CreateEvent;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Ticketing.Presentation.Events;

public sealed class EventPublishedIntegrationEventConsumer(ISender sender)
    : IConsumer<EventPublishedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<EventPublishedIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new CreateEventCommand(
                context.Message.EventId,
                context.Message.Title,
                context.Message.Description,
                context.Message.Location,
                context.Message.StartsAtUtc,
                context.Message.EndsAtUtc,
                context.Message.TicketTypes
                    .Select(t => new CreateEventCommand.TicketTypeRequest(
                        t.Id,
                        context.Message.EventId,
                        t.Name,
                        t.Price,
                        t.Currency,
                        t.Quantity))
                    .ToList()),
            context.CancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateEventCommand), result.Error);
        }
    }
}
