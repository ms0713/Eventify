using Eventify.Modules.Events.Domain.Abstractions;

namespace Eventify.Modules.Events.Domain.Events;
public sealed class EventCanceledDomainEvent(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}
