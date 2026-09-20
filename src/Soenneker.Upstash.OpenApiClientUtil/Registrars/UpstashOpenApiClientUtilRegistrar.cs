using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Upstash.HttpClients.Registrars;
using Soenneker.Upstash.OpenApiClientUtil.Abstract;

namespace Soenneker.Upstash.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class UpstashOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="UpstashOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddUpstashOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddUpstashOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IUpstashOpenApiClientUtil, UpstashOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="UpstashOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddUpstashOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddUpstashOpenApiHttpClientAsSingleton()
                .TryAddScoped<IUpstashOpenApiClientUtil, UpstashOpenApiClientUtil>();

        return services;
    }
}
