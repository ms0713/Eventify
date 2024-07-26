using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Attendance.Application.Attendees.UpdateAttendee;
using Eventify.Modules.Users.IntegrationEvents;
using MediatR;

namespace Eventify.Modules.Attendance.Presentation.Attendees;

public sealed class UserProfileUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserProfileUpdatedIntegrationEvent>
{
    public override async Task Handle(
        UserProfileUpdatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateAttendeeCommand(
                integrationEvent.UserId,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(UpdateAttendeeCommand), result.Error);
        }
    }
}
