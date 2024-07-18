﻿using Eventify.Modules.Events.Application.Abstractions.Messaging;

namespace Eventify.Modules.Events.Application.Events.RescheduleEvent;

public sealed record RescheduleEventCommand(Guid EventId, DateTime StartsAtUtc, DateTime? EndsAtUtc) : ICommand;
