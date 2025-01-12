using Microsoft.EntityFrameworkCore;

public interface IPostRepository
{
    Task<(IEnumerable<Post> posts, int totalCount)> GetPostsAsync(int page, int pageSize);
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
        // Pobierz całkowitą liczbę postów (do wyświetlenia w paginacji)
        int totalCount = await _context.Posts.CountAsync();

        // Zastosuj paginację
        var posts = await _context.Posts
            .Include(p => p.user)
            .Include(p => p.category)
            .OrderByDescending(p => p.dateCreation) // Sortuj od najnowszych
            .Skip((page - 1) * pageSize) // Pomijamy elementy z poprzednich stron
            .Take(pageSize) // Pobieramy określoną liczbę elementów
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

        // Zwróć posty oraz całkowitą liczbę elementów
        return (posts, totalCount);
    }
}
