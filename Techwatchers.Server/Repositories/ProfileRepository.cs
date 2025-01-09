using Microsoft.EntityFrameworkCore;

public interface IProfileRepository
{
    User? GetUserById(int userId);
    User? GetUserByUsername(string username);
    void UpdatePassword(User user, string newPassword);
}

public class ProfileRepository : IProfileRepository
{
    private readonly TechwatchersContext _context;

    public ProfileRepository(TechwatchersContext context)
    {
        _context = context;
    }

    public User? GetUserById(int userId)
    {
        return _context.Users.FirstOrDefault(u => u.id == userId);
    }

    public User? GetUserByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.username == username);
    }

    public void UpdatePassword(User user, string newPassword)
    {
        user.password = newPassword;
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}