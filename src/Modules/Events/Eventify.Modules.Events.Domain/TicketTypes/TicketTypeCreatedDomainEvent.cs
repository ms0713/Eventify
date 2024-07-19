using Eventify.Common.Domain;

namespace Eventify.Modules.Events.Domain.TicketTypes;

public sealed class TicketTypeCreatedDomainEvent(Guid ticketTypeId) : DomainEvent
{
    public Guid TicketTypeId { get; init; } = ticketTypeId;
}
