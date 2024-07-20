using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Ticketing.Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(Guid CustomerId) : ICommand;
