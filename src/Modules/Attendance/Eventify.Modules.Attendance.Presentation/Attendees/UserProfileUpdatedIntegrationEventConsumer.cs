using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Attendance.Application.Attendees.UpdateAttendee;
using Eventify.Modules.Users.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Attendance.Presentation.Attendees;

public sealed class UserProfileUpdatedIntegrationEventConsumer(ISender sender)
    : IConsumer<UserProfileUpdatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserProfileUpdatedIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new UpdateAttendeeCommand(
                context.Message.UserId,
                context.Message.FirstName,
                context.Message.LastName),
            context.CancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(UpdateAttendeeCommand), result.Error);
        }
    }
}
