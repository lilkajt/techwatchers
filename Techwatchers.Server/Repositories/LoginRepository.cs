using Microsoft.EntityFrameworkCore;
public interface ILoginRepository
{
    public User? ValidateUser(string username, string password);
    public string GetUserPassword(string username);
    public Task<User?> GetUserByIdAsync(int id);
}
public class LoginRepository : ILoginRepository
{

    private readonly TechwatchersContext _context;

    public LoginRepository(TechwatchersContext context)
    {
        _context = context;
    }
    public User? ValidateUser(string username, string password)
    {
        return _context.Users.FirstOrDefault(
            u=> u.username == username && u.password == password
        );
    }
    public string? GetUserPassword(string username)
    {
        return _context.Users.FirstOrDefault(u => u.username == username)?.password;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.id == id);
    }
}