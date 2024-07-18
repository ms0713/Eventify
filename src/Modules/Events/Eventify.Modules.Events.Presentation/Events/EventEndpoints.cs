using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.Events;
public static class EventEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateEvent.MapEndpoint(app);
        GetEvent.MapEndpoint(app);
    }
}
