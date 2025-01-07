using Microsoft.EntityFrameworkCore;

public interface IHeaderRepository{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<bool> AddPostAsync(string title, string description, int category_id, int user_id);
    Task<bool> CategoryExistsAsync(int c_id);
    Task<bool> UserExistsAsync(int u_id);
}
public class HeaderRepository : IHeaderRepository
{
    private readonly TechwatchersContext _context;

    public HeaderRepository(TechwatchersContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
    public async Task<bool> AddPostAsync(string title, string description, int category_id, int user_id)
    {
        var post = new Post
        {
            title = title,
            description = description,
            category_id = category_id,
            dateCreation = DateTime.UtcNow,
            user_id = user_id
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return true;
    }
    public async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _context.Categories.AnyAsync(c => c.id == categoryId);
    }
    public async Task<bool> UserExistsAsync(int userId)
    {
        return await _context.Users.AnyAsync(u => u.id == userId);
    }
}