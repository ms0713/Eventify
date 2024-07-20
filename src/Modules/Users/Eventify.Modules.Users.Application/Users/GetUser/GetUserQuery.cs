using Eventify.Common.Application.Messaging;

namespace Eventify.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
