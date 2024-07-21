using System.Net;
using Eventify.Common.Domain;
using Eventify.Modules.Users.Application.Abstractions.Identity;
using Microsoft.Extensions.Logging;

namespace Eventify.Modules.Users.Infrastructure.Identity;
internal sealed class IdentityServiceProvider(
    KeyCloakClient keyCloakClient,
    ILogger<IdentityServiceProvider> logger)
    : IIdentityProviderService
{
    private const string PasswordCredentialType = "Password";

    // POST /admin/realms/{realm}/users
    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Email,
            user.Email,
            user.FirstName,
            user.LastName,
            true,
            true,
            [new CredentialRepresentation(PasswordCredentialType, user.Password, false)]);

        try
        {
            string identityId = await keyCloakClient.RegisterUserAsync(
                userRepresentation,
                cancellationToken);

            return identityId;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(ex, "User registration failed.");

            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }
}
