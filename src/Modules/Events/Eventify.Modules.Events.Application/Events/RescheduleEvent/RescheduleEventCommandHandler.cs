﻿using Eventify.Modules.Events.Application.Abstractions.Clock;
using Eventify.Modules.Events.Application.Abstractions.Data;
using Eventify.Modules.Events.Application.Abstractions.Messaging;
using Eventify.Modules.Events.Domain.Abstractions;
using Eventify.Modules.Events.Domain.Events;

namespace Eventify.Modules.Events.Application.Events.RescheduleEvent;

internal sealed class RescheduleEventCommandHandler(
    IDateTimeProvider dateTimeProvider,
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RescheduleEventCommand>
{
    public async Task<Result> Handle(RescheduleEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        if (request.StartsAtUtc < dateTimeProvider.UtcNow)
        {
            return Result.Failure(EventErrors.StartDateInPast);
        }

        @event.Reschedule(request.StartsAtUtc, request.EndsAtUtc);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
