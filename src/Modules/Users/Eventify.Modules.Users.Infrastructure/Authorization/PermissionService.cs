using Eventify.Common.Application.Authorization;
using Eventify.Common.Domain;
using Eventify.Modules.Users.Application.Users.GetUserPermissions;
using MediatR;

namespace Eventify.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}
