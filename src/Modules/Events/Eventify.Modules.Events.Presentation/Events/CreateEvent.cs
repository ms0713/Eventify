﻿using Eventify.Common.Domain;
using Eventify.Common.Presentation.Endpoints;
using Eventify.Common.Presentation.Results;
using Eventify.Modules.Events.Application.Events.CreateEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.Events;
internal sealed class CreateEvent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateEventCommand(
                request.CategoryId,
                request.Title,
                request.Description,
                request.Location,
                request.StartsAtUtc,
                request.EndsAtUtc));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization().WithTags(Tags.Events);
    }

    internal sealed class Request
    {
        public Guid CategoryId { get; init; }

        public string Title { get; init; }

        public string Description { get; init; }

        public string Location { get; init; }

        public DateTime StartsAtUtc { get; init; }

        public DateTime? EndsAtUtc { get; init; }
    }
}
