using Eventify.Common.Application.Authorization;
using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Users.Application.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
