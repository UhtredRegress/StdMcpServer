using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SampleMcpServer;

public class OdooClient: IOdooClient
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<OdooClient> _logger;
    private readonly HttpClient _httpClient;

    public OdooClient(IConfiguration configuration, ILogger<OdooClient> logger,  HttpClient httpClient)
    {
        _configuration = configuration;
        _logger = logger;
        _httpClient = httpClient;
    }
    
    public async Task<int> LoginAsync(string username, string password, string database)
    {
        _logger.LogInformation("Odoo client logged in");
        var payload = new
        {
            jsonrpc = "2.0",
            method = "call",
            @params = new
            {
                service = "common",
                method = "login",
                args = new object[] { database, username, password }
            },
            id = 1
        };

        var response = await _httpClient.PostAsync("http://localhost:8069/jsonrpc",
            new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));
        
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("result").GetInt32();
       
    }
}