using Eventify.Common.Application.Clock;
using Eventify.Common.Application.Data;
using Eventify.Common.Infrastructure.Clock;
using Eventify.Common.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Eventify.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string databaseConnectionString)
    {

        NpgsqlDataSource npgsqlDataSource =
            new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
