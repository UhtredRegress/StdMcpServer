using ModelContextProtocol.Server;

namespace SampleMcpServer.Tools;

public class AuthorService
{
    private readonly ApplicationDbContext _context;

    public AuthorService(ApplicationDbContext context)
    {
        _context = context;
    }

    [McpServerTool]
    public async Task<Author> CreateNewAuthor(string authorName)
    {
        try
        {
            var saveAuthor = _context.Authors.Add(new Author { Name = authorName });
            await _context.SaveChangesAsync();
            return saveAuthor.Entity;
        }
        catch (Exception ex)
        {
            return new Author { Name = ""};
        }
    }
}