using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Events.Application.TicketTypes.GetTicketType;

public sealed record GetTicketTypeQuery(Guid TicketTypeId) : IQuery<TicketTypeResponse>;
