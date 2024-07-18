﻿using Eventify.Modules.Events.Application.Events.PublishEvent;
using Eventify.Modules.Events.Domain.Abstractions;
using Eventify.Modules.Events.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.Events;

internal static class PublishEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("events/{id}/publish", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new PublishEventCommand(id));

            return result.Match(Results.NoContent, ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Events);
    }
}
