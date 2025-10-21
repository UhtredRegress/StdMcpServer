using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;

namespace SampleMcpServer.Tests;

public class OdooClientTest
{
    private readonly ITestOutputHelper _output;

    public OdooClientTest(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public async Task Test1()
    {
        var mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(c => c["Base_Url"]).Returns("http://localhost:8069");
        
        var mockLogger = new Mock<ILogger<OdooClient>>();
        
        var mockHttp = new HttpClient();
        
        var test = new OdooClient(mockConfig.Object, mockLogger.Object, mockHttp);

        var result = await test.LoginAsync("test@gmail.com", "odoo", "db");
        _output.WriteLine(result.ToString());
        Assert.IsType<int>(result);
    }
}
