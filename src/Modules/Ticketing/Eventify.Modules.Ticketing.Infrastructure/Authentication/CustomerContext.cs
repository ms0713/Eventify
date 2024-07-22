using Eventify.Common.Application.Exceptions;
using Eventify.Common.Infrastructure.Authentication;
using Eventify.Modules.Ticketing.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Eventify.Modules.Ticketing.Infrastructure.Authentication;

internal sealed class CustomerContext(IHttpContextAccessor httpContextAccessor) : ICustomerContext
{
    public Guid CustomerId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                              throw new EventifyException("User identifier is unavailable");
}
