using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleMcpServer;
using SampleMcpServer.Tools;
using Serilog;

Serilog.Debugging.SelfLog.Enable(msg => Console.Error.WriteLine(msg));
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs/mcp-server.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();
var builder = Host.CreateApplicationBuilder(args);


Env.Load();
builder.Configuration.AddEnvironmentVariables();
// Configure all logs to go to stderr (stdout is used for the MCP protocol messages).
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
builder.Services.AddHttpClient<IOdooClient, OdooClient>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration["DefaultConnection"]));

Log.Information("Starting host at the program.cs");
// Add the MCP services: the transport to use (stdio) and the tools to register.
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<RandomNumberTools>()
    .WithTools<OdooTools>();

await builder.Build().RunAsync();
