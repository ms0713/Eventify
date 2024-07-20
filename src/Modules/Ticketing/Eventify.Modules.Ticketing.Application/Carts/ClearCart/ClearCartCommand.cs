using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Ticketing.Application.Carts.ClearCart;

public sealed record ClearCartCommand(Guid CustomerId) : ICommand;
