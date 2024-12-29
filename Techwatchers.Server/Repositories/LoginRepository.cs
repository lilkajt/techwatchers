using Microsoft.EntityFrameworkCore;
public interface ILoginRepository
{
    public User? ValidateUser(string username, string password);
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
}