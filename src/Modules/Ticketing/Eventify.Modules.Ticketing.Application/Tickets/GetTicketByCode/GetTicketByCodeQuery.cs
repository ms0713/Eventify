using Eventify.Common.Application.Messaging;
using Eventify.Modules.Ticketing.Application.Tickets.GetTicket;

namespace Eventify.Modules.Ticketing.Application.Tickets.GetTicketByCode;

public sealed record GetTicketByCodeQuery(string Code) : IQuery<TicketResponse>;
