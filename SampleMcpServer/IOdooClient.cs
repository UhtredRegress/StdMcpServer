namespace SampleMcpServer;

public interface IOdooClient
{
    Task<int> LoginAsync(string username, string password, string database);
}