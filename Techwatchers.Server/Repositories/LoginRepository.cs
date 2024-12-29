using Microsoft.EntityFrameworkCore;

public interface ILoginRepository
{
    // Task<IEnumerable<User>> GetLoginsAsync();
}

public class loginRepository : ILoginRepository
{
    private readonly TechwatchersContext _context;

    public loginRepository(TechwatchersContext context)
    {
        _context = context;
    }
    
}