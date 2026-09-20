using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Upstash.HttpClients.Abstract;
using Soenneker.Upstash.OpenApiClientUtil.Abstract;
using Soenneker.Upstash.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Upstash.OpenApiClientUtil;

public sealed class UpstashOpenApiClientUtil : IUpstashOpenApiClientUtil
{
    private readonly AsyncSingleton<UpstashOpenApiClient> _client;

    public UpstashOpenApiClientUtil(IUpstashOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<UpstashOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            // Authentication is configured once on the shared HTTP client.
            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient)
            {
                BaseUrl = httpClient.BaseAddress!.AbsoluteUri.TrimEnd('/')
            };

            return new UpstashOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<UpstashOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
