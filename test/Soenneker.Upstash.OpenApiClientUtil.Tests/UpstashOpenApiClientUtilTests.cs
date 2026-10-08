using System;
using System.Threading.Tasks;
using Soenneker.Upstash.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;
using System.Threading;

namespace Soenneker.Upstash.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class UpstashOpenApiClientUtilTests(Host host) : HostedUnitTest(host)
{
    [Test]
    public async ValueTask Generated_requests_use_the_configured_base_url(CancellationToken cancellationToken)
    {
        var client = await Resolve<IUpstashOpenApiClientUtil>(true).Get(cancellationToken: cancellationToken);
        var request = client.Redis.Databases.ToGetRequestInformation();
        await Assert.That(request.URI.AbsoluteUri).IsEqualTo("https://upstash.example.test/v2/redis/databases");
    }

    [Test]
    public async ValueTask Get_reuses_the_generated_client(CancellationToken cancellationToken)
    {
        var util = Resolve<IUpstashOpenApiClientUtil>(true);
        var first = await util.Get(cancellationToken: cancellationToken);
        var second = await util.Get(cancellationToken: cancellationToken);
        await Assert.That(ReferenceEquals(first, second)).IsTrue();
    }
}
