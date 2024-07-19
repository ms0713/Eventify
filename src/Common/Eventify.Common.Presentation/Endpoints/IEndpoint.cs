using Microsoft.AspNetCore.Routing;

namespace Eventify.Common.Presentation.Endpoints;
public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
