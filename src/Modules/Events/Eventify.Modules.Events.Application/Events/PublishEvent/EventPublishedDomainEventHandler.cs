using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Exceptions;
using Eventify.Common.Application.Messaging;
using Eventify.Common.Domain;
using Eventify.Modules.Events.Application.Events.GetEvent;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.IntegrationEvents;
using MediatR;

namespace Eventify.Modules.Events.Application.Events.PublishEvent;

internal sealed class EventPublishedDomainEventHandler(ISender sender, IEventBus eventBus)
    : IDomainEventHandler<EventPublishedDomainEvent>
{
    public async Task Handle(EventPublishedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        Result<EventResponse> result = await sender.Send(new GetEventQuery(domainEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(GetEventQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new EventPublishedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Title,
                result.Value.Description,
                result.Value.Location,
                result.Value.StartsAtUtc,
                result.Value.EndsAtUtc,
                result.Value.TicketTypes.Select(t => new TicketTypeModel
                {
                    Id = t.TicketTypeId,
                    EventId = result.Value.Id,
                    Name = t.Name,
                    Price = t.Price,
                    Currency = t.Currency,
                    Quantity = t.Quantity
                }).ToList()),
            cancellationToken);
    }
}
