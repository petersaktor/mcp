using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace SimpleServer.Prompts;

[McpServerPromptType]
public static class SimplePrompt
{
    [McpServerPrompt, Description("Creates a prompt to summarize the provided message.")]
    public static ChatMessage Summarize(string content) =>
        new(ChatRole.User, $"Please summarize this content into a single sentence: {content}");
}
