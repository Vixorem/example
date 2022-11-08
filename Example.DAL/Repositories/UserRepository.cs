using Example.DAL.Converters;

namespace Example.DAL.Repositories;

/// <summary>
/// Реализовали/имплементировали интерфейс
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public User? GetByLogin(string login)
    {
        // FirstOrDefault вернет либо одну запись, либо нуль
        var user = _context.Users.FirstOrDefault(u => u.Login == login);
        return user?.ToDomain();
    }
}