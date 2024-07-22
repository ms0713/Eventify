using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Ticketing.Application.Customers.UpdateCustomer;
using Eventify.Modules.Users.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Ticketing.Presentation.Customers;

public sealed class UserProfileUpdatedIntegrationEventConsumer(ISender sender)
    : IConsumer<UserProfileUpdatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserProfileUpdatedIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new UpdateCustomerCommand(
                context.Message.UserId,
                context.Message.FirstName,
                context.Message.LastName),
            context.CancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(UpdateCustomerCommand), result.Error);
        }
    }
}
