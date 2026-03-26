using Soenneker.Notion.HttpClients.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Notion.HttpClients.Tests;

[Collection("Collection")]
public sealed class NotionOpenApiHttpClientTests : FixturedUnitTest
{
    private readonly INotionOpenApiHttpClient _httpclient;

    public NotionOpenApiHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<INotionOpenApiHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
