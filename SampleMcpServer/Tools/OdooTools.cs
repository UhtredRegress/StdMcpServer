using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace SampleMcpServer.Tools;

[McpServerToolType]
public class OdooTools
{
    private readonly IOdooClient _client;
    private readonly ILogger<OdooTools> _logger;

    public OdooTools(IOdooClient client, ILogger<OdooTools> logger)
    {
        _client = client;
        _logger = logger;
    }
    
    [McpServerTool]
    public async Task<int> LoginOdoo(string username, string password, string database)
    {
        _logger.LogInformation("LoginOdoo");
        try
        {
            return await _client.LoginAsync(username, password, database);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "LoginOdoo");
            return -1; 
        }
    }
}