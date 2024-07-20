﻿using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Tickets.ArchiveTicketsForEvent;

internal sealed class ArchiveTicketsForEventCommandValidator : AbstractValidator<ArchiveTicketsForEventCommand>
{
    public ArchiveTicketsForEventCommandValidator()
    {
        RuleFor(c => c.EventId).NotEmpty();
    }
}
