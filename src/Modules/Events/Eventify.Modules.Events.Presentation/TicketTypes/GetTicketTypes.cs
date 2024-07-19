using Eventify.Common.Domain;
using Eventify.Common.Presentation.ApiResults;
using Eventify.Common.Presentation.Endpoints;
using Eventify.Modules.Events.Application.TicketTypes.GetTicketType;
using Eventify.Modules.Events.Application.TicketTypes.GetTicketTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.TicketTypes;

internal sealed class GetTicketTypes : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("ticket-types", async (Guid eventId, ISender sender) =>
        {
            Result<IReadOnlyCollection<TicketTypeResponse>> result = await sender.Send(
                new GetTicketTypesQuery(eventId));

            return result.Match(Results.Ok, Common.Presentation.ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.TicketTypes);
    }
}
