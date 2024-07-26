using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Application.Customers.CreateCustomer;
using Eventify.Modules.Users.IntegrationEvents;
using MediatR;

namespace Eventify.Modules.Ticketing.Presentation.Customers;

public sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateCustomerCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName), 
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}
