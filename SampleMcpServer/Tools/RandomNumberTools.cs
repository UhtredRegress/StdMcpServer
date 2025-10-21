using System.ComponentModel;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

/// <summary>
/// Sample MCP tools for demonstration purposes.
/// These tools can be invoked by MCP clients to perform various operations.
/// </summary>
[McpServerToolType]
internal class RandomNumberTools
{
    private readonly ILogger<RandomNumberTools> _logger;

    public RandomNumberTools(ILogger<RandomNumberTools> logger)
    {
        _logger = logger;
    }
    [McpServerTool]
    [Description("Generates a random number between the specified minimum and maximum values.")]
    public int GetRandomNumber(
        [Description("Minimum value (inclusive)")] int min = 0,
        [Description("Maximum value (exclusive)")] int max = 100)
    {
        _logger.LogInformation($"Generating random number between {min}-{max}.");
        return Random.Shared.Next(min, max);
    }
}
