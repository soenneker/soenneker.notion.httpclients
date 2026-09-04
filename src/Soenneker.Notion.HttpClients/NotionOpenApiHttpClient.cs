using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Notion.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Notion.HttpClients;

/// <inheritdoc cref="INotionOpenApiHttpClient" />
public sealed class NotionOpenApiHttpClient : INotionOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;
    private readonly string _clientId = $"{nameof(NotionOpenApiHttpClient)}:{Guid.NewGuid():N}";

    private const string _prodBaseUrl = "https://api.notion.com/";

    public NotionOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_clientId, (config: _config, baseUrl: _config["Notion:ClientBaseUrl"] ?? _prodBaseUrl), static state =>
        {
            var apiKey = state.config.GetValueStrict<string>("Notion:ApiKey");
            string authHeaderName = state.config["Notion:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = state.config["Notion:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);
            string apiVersion = state.config["Notion:ApiVersion"] ?? "2026-03-11";

            return new HttpClientOptions
            {
                BaseAddress = new Uri(state.baseUrl),
                DefaultRequestHeaders = new Dictionary<string, string>
                {
                    {authHeaderName, authHeaderValue},
                    {"Notion-Version", apiVersion},
                }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_clientId);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_clientId);
    }
}
