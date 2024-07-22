using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Application.Customers.CreateCustomer;
using Eventify.Modules.Users.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Ticketing.Presentation.Customers;

public sealed class UserRegisteredIntegrationEventConsumer(ISender sender)
    : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new CreateCustomerCommand(
                context.Message.UserId,
                context.Message.Email,
                context.Message.FirstName,
                context.Message.LastName), 
            context.CancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}
