[![](https://img.shields.io/nuget/v/soenneker.notion.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.notion.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.notion.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.notion.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.notion.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.notion.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.notion.httpclients/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.notion.httpclients/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Notion.HttpClients

Provides a cached `HttpClient` with Notion API authentication and version headers.

## Installation

```bash
dotnet add package Soenneker.Notion.HttpClients
```

## Configuration

```json
{
  "Notion": {
    "ApiKey": "your-integration-token"
  }
}
```

The client sends `Authorization: Bearer {token}` and `Notion-Version: 2026-03-11`, matching the schema used by the generated client. `Notion:ClientBaseUrl`, `Notion:AuthHeaderName`, `Notion:AuthHeaderValueTemplate`, and `Notion:ApiVersion` can override those defaults.

## Usage

```csharp
using Soenneker.Notion.HttpClients.Abstract;
using Soenneker.Notion.HttpClients.Registrars;

services.AddNotionOpenApiHttpClientAsSingleton();

INotionOpenApiHttpClient notion = serviceProvider
    .GetRequiredService<INotionOpenApiHttpClient>();

HttpClient client = await notion.Get(cancellationToken);
```

Do not dispose the returned `HttpClient`; the registered provider owns it and removes it from the cache when disposed.
