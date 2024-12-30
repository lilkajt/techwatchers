using Microsoft.EntityFrameworkCore;
public interface IRegisterRepository
{
    bool IsUsernameOrEmailTaken(string username, string email);
    void RegisterUser(User user);
}
public class RegisterRepository : IRegisterRepository
{

    private readonly TechwatchersContext _context;

    public RegisterRepository(TechwatchersContext context)
    {
        _context = context;
    }
    public bool IsUsernameOrEmailTaken(string username, string email)
    {
        return _context.Users.Any(u => u.username == username || u.email == email);
    }

    public void RegisterUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

}