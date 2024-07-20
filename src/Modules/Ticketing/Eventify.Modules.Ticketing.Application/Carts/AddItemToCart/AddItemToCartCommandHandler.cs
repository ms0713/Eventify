﻿using Eventify.Common.Application.Messaging;
using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Domain.Customers;
using Eventify.Modules.Ticketing.Domain.Events;

namespace Eventify.Modules.Ticketing.Application.Carts.AddItemToCart;

internal sealed class AddItemToCartCommandHandler(
    ICustomerRepository customerRepository,
    ITicketTypeRepository ticketTypeRepository,
    CartService cartService)
    : ICommandHandler<AddItemToCartCommand>
{
    public async Task<Result> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        TicketType? ticketType = await ticketTypeRepository.GetAsync(request.TicketTypeId, cancellationToken);

        if (ticketType is null)
        {
            return Result.Failure(TicketTypeErrors.NotFound(request.TicketTypeId));
        }

        if (ticketType.AvailableQuantity < request.Quantity)
        {
            return Result.Failure(TicketTypeErrors.NotEnoughQuantity(ticketType.AvailableQuantity));
        }

        var cartItem = new CartItem
        {
            TicketTypeId = request.TicketTypeId,
            Quantity = request.Quantity,
            Price = ticketType.Price,
            Currency = ticketType.Currency
        };

        await cartService.AddItemAsync(request.CustomerId, cartItem, cancellationToken);

        return Result.Success();
    }
}