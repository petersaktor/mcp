using ModelContextProtocol.Server;
using System.ComponentModel;

namespace SimpleServer.Prompts;

[McpServerResourceType]
public static class SimpleResource
{
    [McpServerResource, Description("Returns the Bing endpoint URL.")]
    public static string GetBingEndpoint() => "https://bing.com";
}
