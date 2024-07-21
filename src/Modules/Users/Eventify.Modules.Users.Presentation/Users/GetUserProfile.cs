using System.Security.Claims;
using Eventify.Common.Domain;
using Eventify.Common.Infrastructure.Authentication;
using Eventify.Common.Presentation.Endpoints;
using Eventify.Common.Presentation.Results;
using Eventify.Modules.Users.Application.Users.GetUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/profile", async (ClaimsPrincipal claims, ISender sender) =>
        {
            Result<UserResponse> result = await sender.Send(new GetUserQuery(claims.GetUserId()));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization("users:read")
        .WithTags(Tags.Users);
    }
}
