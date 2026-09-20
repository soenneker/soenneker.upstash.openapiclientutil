# Soenneker.Upstash.OpenApiClientUtil

Dependency-injection registration and cached access to the generated Upstash Developer API client (.NET 10).

```csharp
using Soenneker.Upstash.OpenApiClientUtil.Abstract;
using Soenneker.Upstash.OpenApiClientUtil.Registrars;

services.AddUpstashOpenApiClientUtilAsSingleton();
// Resolve IUpstashOpenApiClientUtil through dependency injection.
var client = await upstashClientUtil.Get(cancellationToken);
var databases = await client.Redis.Databases.GetAsync(cancellationToken: cancellationToken);
```

Configure `Upstash:Email` and `Upstash:ApiKey` with your native account's Developer API credentials. Store these in secret configuration, or set `Upstash__Email` and `Upstash__ApiKey` environment variables. Basic authentication is configured by the HTTP-client library; callers do not need to pre-encode credentials.

`Upstash:ClientBaseUrl` overrides the default `https://api.upstash.com/v2/` for both HTTP and generated requests. Use a trusted HTTPS endpoint. Scoped registration is available through `AddUpstashOpenApiClientUtilAsScoped()`.

The generated client manages Redis databases, Vector indexes, Search, QStash account resources, and teams exposed by the Developer API. Redis data commands and the separate QStash messaging API are outside this specification.

## Local development

Place the four suite repositories alongside each other. Build against sibling projects before publishing the initial dependency packages:

```powershell
dotnet build -p:UseLocalUpstashProjects=true
dotnet test --project test/Soenneker.Upstash.OpenApiClientUtil.Tests -p:UseLocalUpstashProjects=true -- --treenode-filter '/*/*/UpstashOpenApiClientUtilTests/*'
```

Normal consumer builds use NuGet package references. Publish the generated client and HTTP client packages before the utility package.
