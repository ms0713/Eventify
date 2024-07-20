using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Ticketing.Application.Carts.GetCart;

public sealed record GetCartQuery(Guid CustomerId) : IQuery<Cart>;
