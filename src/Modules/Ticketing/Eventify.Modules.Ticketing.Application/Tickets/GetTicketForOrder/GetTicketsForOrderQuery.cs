using Eventify.Common.Application.Messaging;
using Eventify.Modules.Ticketing.Application.Tickets.GetTicket;

namespace Eventify.Modules.Ticketing.Application.Tickets.GetTicketForOrder;

public sealed record GetTicketsForOrderQuery(Guid OrderId) : IQuery<IReadOnlyCollection<TicketResponse>>;
