using Eventify.Common.Application.EventBus;
using Eventify.Common.Application.Exceptions;
using Eventify.Common.Domain;
using Eventify.Modules.Attendance.Application.Attendees.CreateAttendee;
using Eventify.Modules.Users.IntegrationEvents;
using MassTransit;
using MediatR;

namespace Eventify.Modules.Attendance.Presentation.Attendees;

public sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateAttendeeCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(CreateAttendeeCommand), result.Error);
        }
    }
}
