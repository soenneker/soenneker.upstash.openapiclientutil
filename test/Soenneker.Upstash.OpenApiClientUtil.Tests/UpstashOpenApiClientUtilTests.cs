using System;
using System.Threading.Tasks;
using Soenneker.Upstash.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Upstash.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class UpstashOpenApiClientUtilTests(Host host) : HostedUnitTest(host)
{
    [Test]
    public async Task Generated_requests_use_the_configured_base_url()
    {
        var client = await Resolve<IUpstashOpenApiClientUtil>(true).Get();
        var request = client.Redis.Databases.ToGetRequestInformation();
        await Assert.That(request.URI.AbsoluteUri).IsEqualTo("https://upstash.example.test/v2/redis/databases");
    }

    [Test]
    public async Task Get_reuses_the_generated_client()
    {
        var util = Resolve<IUpstashOpenApiClientUtil>(true);
        var first = await util.Get();
        var second = await util.Get();
        await Assert.That(ReferenceEquals(first, second)).IsTrue();
    }
}
