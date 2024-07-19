﻿using Eventify.Common.Domain;

namespace Eventify.Modules.Events.Domain.Events;
public sealed class EventPublishedDomainEvent(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}
