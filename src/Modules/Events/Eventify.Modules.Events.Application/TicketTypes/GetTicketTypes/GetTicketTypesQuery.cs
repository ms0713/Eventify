using Eventify.Common.Application.Messaging;
using Eventify.Modules.Events.Application.TicketTypes.GetTicketType;

namespace Eventify.Modules.Events.Application.TicketTypes.GetTicketTypes;

public sealed record GetTicketTypesQuery(Guid EventId) : IQuery<IReadOnlyCollection<TicketTypeResponse>>;
