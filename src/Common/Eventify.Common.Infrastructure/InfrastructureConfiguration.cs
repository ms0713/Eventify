using Eventify.Common.Application.Caching;
using Eventify.Common.Application.Clock;
using Eventify.Common.Application.Data;
using Eventify.Common.Application.EventBus;
using Eventify.Common.Infrastructure.Caching;
using Eventify.Common.Infrastructure.Clock;
using Eventify.Common.Infrastructure.Data;
using Eventify.Common.Infrastructure.Outbox;
using Eventify.Common.Infrastructure.Authentication;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;
using Eventify.Common.Infrastructure.Authorization;
using Dapper;
using Quartz;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Eventify.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string serviceName,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        string databaseConnectionString,
        string redisConnectionString)
    {
        services.AddAuthenticationInternal();

        services.AddAuthorizationInternal();

        NpgsqlDataSource npgsqlDataSource =
            new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        SqlMapper.AddTypeHandler(new GenericArrayHandler<string>());

        services.AddQuartz(configurator =>
        {
            var scheduler = Guid.NewGuid();
            configurator.SchedulerId = $"default-id-{scheduler}";
            configurator.SchedulerName = $"default-name-{scheduler}";
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.AddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<ICacheService, CacheService>();

        services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        services.AddMassTransit(configure =>
        {
            foreach (Action<IRegistrationConfigurator> configureConsumers in moduleConfigureConsumers)
            {
                configureConsumers(configure);
            }

            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services
        .AddOpenTelemetry()
        .ConfigureResource(resource => resource.AddService(serviceName))
        .WithTracing(tracing =>
        {
            tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddRedisInstrumentation()
                .AddNpgsql()
                .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName);

            tracing.AddOtlpExporter();
        });

        return services;
    }
}
