using Microsoft.EntityFrameworkCore;

public interface IPostRepository
{
    Task<(IEnumerable<Post> posts, int totalCount)> GetPostsAsync(int page, int pageSize);
    Task<Post?> GetPostByIdAsync(int id);
    Task AddLikeAsync(int postId);
    Task RemoveLikeAsync(int postId);
    Task<IEnumerable<PostComment>> GetCommentsByPostIdAsync(int postId);
    Task<PostComment> AddCommentAsync(int postId, int userId, string commentContent);
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
        IQueryable<Post> query = _context.Posts
            .Include(p => p.user)
            .Include(p => p.category)
            .OrderByDescending(p => p.dateCreation);

        int totalCount = await query.CountAsync();

        var posts = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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

    public async Task<IEnumerable<PostComment>> GetCommentsByPostIdAsync(int postId)
    {
        IQueryable<PostComment> query = _context.PostComments
            .Include(c => c.user)
            .Where(c => c.post_id == postId)
            .OrderByDescending(c => c.dateCreation);

        var comments = await query.ToListAsync();
        return comments;
    }

public async Task<PostComment> AddCommentAsync(int postId, int userId, string commentContent)
{
    var comment = new PostComment
    {
        post_id = postId,
        user_id = userId,
        comment_content = commentContent,
        dateCreation = DateTime.UtcNow
    };

    _context.PostComments.Add(comment);
    await _context.SaveChangesAsync();

    return comment;
}

}

