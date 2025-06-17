using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;
using System;
using System.ComponentModel;

namespace SimpleServer.Prompts;

[McpServerToolType]
public static class SimpleTool
{
    [McpServerTool, Description("Returns the current time for the specified time zone.")]
    public static string GetCurrentTime(string timeZone)
    {
        try
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            return now.ToString("o"); // ISO 8601 format
        }
        catch (TimeZoneNotFoundException)
        {
            return $"Time zone '{timeZone}' not found.";
        }
        catch (InvalidTimeZoneException)
        {
            return $"Time zone '{timeZone}' is invalid.";
        }
    }
}
