using Microsoft.EntityFrameworkCore;

public interface IPostRepository
{
    Task<(IEnumerable<Post> posts, int totalCount)> GetPostsAsync(int page, int pageSize);
    Task<Post?> GetPostByIdAsync(int id);
    Task AddLikeAsync(int postId);
    Task RemoveLikeAsync(int postId);
}

public class PostRepository : IPostRepository
{
    private readonly TechwatchersContext _context;

    public PostRepository(TechwatchersContext context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Post> posts, int totalCount)> GetPostsAsync(int page, int pageSize)
    {
        int totalCount = await _context.Posts.CountAsync();

        var posts = await _context.Posts
            .Include(p => p.user)
            .Include(p => p.category)
            .OrderByDescending(p => p.dateCreation)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new Post
            {
                id = p.id,
                title = p.title,
                description = p.description,
                dateCreation = p.dateCreation,
                user = p.user,
                category = p.category,
                likes = p.likes
            })
            .ToListAsync();

        return (posts, totalCount);
    }

    public async Task<Post?> GetPostByIdAsync(int id)
    {
        return await _context.Posts
            .Include(p => p.user)
            .Include(p => p.category)
            .FirstOrDefaultAsync(p => p.id == id);
    }

    public async Task AddLikeAsync(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.id == postId);

        if (post == null)
        {
            throw new ArgumentException("Post not found");
        }

        post.likes++;
        await _context.SaveChangesAsync();
    }

    public async Task RemoveLikeAsync(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.id == postId);

        if (post == null)
        {
            throw new ArgumentException("Post not found");
        }

        if (post.likes > 0)
        {
            post.likes--;
            await _context.SaveChangesAsync();
        }
    }
}

