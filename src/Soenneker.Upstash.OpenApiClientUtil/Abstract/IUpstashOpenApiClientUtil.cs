using Soenneker.Upstash.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Upstash.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IUpstashOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the shared generated Developer API client, using the HTTP client's authentication and base URL.
    /// </summary>
    ValueTask<UpstashOpenApiClient> Get(CancellationToken cancellationToken = default);
}
