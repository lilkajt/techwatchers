using Microsoft.EntityFrameworkCore;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetPostsAsync();
}

public class PostRepository : IPostRepository
{
    private readonly TechwatchersContext _context;

    public PostRepository(TechwatchersContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        return await _context.Posts
            .Include(p => p.user)
            .Include(p => p.category)
            .Select(p => new Post
            {
                id = p.id,
                title = p.title,
                description = p.description,
                dateCreation = p.dateCreation,
                user = p.user,
                category = p.category
            })
            .ToListAsync();
    }
}
