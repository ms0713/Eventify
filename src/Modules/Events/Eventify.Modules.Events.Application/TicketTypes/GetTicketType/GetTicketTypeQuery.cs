using Eventify.Modules.Events.Application.Abstractions.Messaging;

namespace Eventify.Modules.Events.Application.TicketTypes.GetTicketType;

public sealed record GetTicketTypeQuery(Guid TicketTypeId) : IQuery<TicketTypeResponse>;
