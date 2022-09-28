namespace Example;

public interface IUserRepository
{
    User? GetByLogin(string login);
}