using Soenneker.Notion.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Notion.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class NotionOpenApiHttpClientTests : HostedUnitTest
{
    private readonly INotionOpenApiHttpClient _httpclient;

    public NotionOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<INotionOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
