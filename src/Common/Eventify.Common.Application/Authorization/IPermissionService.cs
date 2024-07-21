using Eventify.Common.Domain;

namespace Eventify.Common.Application.Authorization;

public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}
