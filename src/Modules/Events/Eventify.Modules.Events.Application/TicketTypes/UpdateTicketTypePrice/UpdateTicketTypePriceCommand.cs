
using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
public sealed record UpdateTicketTypePriceCommand(Guid TicketTypeId, decimal Price) : ICommand;
