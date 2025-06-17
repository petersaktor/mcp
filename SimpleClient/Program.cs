using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

Console.WriteLine($"Hello, MCP Simple client!");

// Create the stdio transport (using current process's stdin/stdout)
var transport = new StdioClientTransport(new StdioClientTransportOptions
{
    Name = "MCP Simple Client",
    Command = "..\\..\\..\\..\\SimpleServer\\bin\\Debug\\net8.0\\SimpleServer.exe",
    // The command to start the MCP server process
    // If the server is already running, you can use the same command as the server
    // If you want to use a different command, specify it here
});

try
{
    // Create the MCP client
    var client = await McpClientFactory.CreateAsync(cancellationToken: default, clientTransport: transport);

    // List available prompts from the server
    foreach (var pr in await client.ListPromptsAsync())
    {
        Console.WriteLine($"Available prompt: {pr.Name} - {pr.Description}");
    }

    // Specify the prompt you want to use
    var promptName = "Summarize";

    // Create a dictionary of arguments for the prompt
    IReadOnlyDictionary<string, object> promptArguments = new Dictionary<string, object>()
    {
       { "content", "ModelContextProtocol enables structured communication." }
    };

    // Get the prompt from the server using the specified name and arguments
    var prompt = await client.GetPromptAsync(promptName, promptArguments!);
    if (prompt == null)
    {
        Console.WriteLine($"Prompt '{promptName}' not found.");
        return;
    }

    // Print the prompt details
    foreach (var message in prompt.Messages)
    {
        Console.WriteLine($"Message Role: {message.Role}, Content: {message.Content?.Text}");
    }

    // List available tools from the server
    foreach (var to in await client.ListToolsAsync())
    {
        Console.WriteLine($"Available tool: {to.Name} - {to.Description}");
    }

    // Specify the tool you want to use
    var toolName = "GetCurrentTime";

    // Create a dictionary of arguments for the tool
    IReadOnlyDictionary<string, object> toolArguments = new Dictionary<string, object>()
    {
        { "timeZone", "Pacific Standard Time" } // Example time zone
    };

    // Call tool from the server using the specified name and arguments
    var result = await client.CallToolAsync(toolName, toolArguments!);
    if (result == null)
    {
        Console.WriteLine($"Tool '{toolName}' not found.");
        return;
    }

    // Print the tool result
    foreach (var res in result.Content)
    {
        Console.WriteLine($"Tool Result: {res.Text}");
    }

    // List available resources from the server
    foreach (var res in await client.ListResourcesAsync())
    {
        Console.WriteLine($"Available resource: {res.Name} - {res.Description}");
    }

    // Specify the resource you want to use
    var resourceName = "resource://GetBingEndpoint";

    // Get the resource from the server using the specified name
    var resource = await client.ReadResourceAsync(resourceName);
    if (resource == null)
    {
        Console.WriteLine($"Resource '{resourceName}' not found.");
        return;
    }

    // Print the resource details
    foreach (var res in resource.Contents)
    {
        Console.WriteLine($"Resource Content: {((TextResourceContents)res)?.Text}");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error creating MCP client: " + ex.Message);
    return;
}
