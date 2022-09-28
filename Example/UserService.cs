namespace Example;

/// <summary>
/// Пример с юзером
/// </summary>
public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public Result<User> GetUserByLogin(string login)
    {
        if (string.IsNullOrEmpty(login))
            return Result.Fail<User>("Логин не был указан");

        var user = _repository.GetByLogin(login);

        return user is null ? Result.Fail<User>("Пользователь не найден") : Result.Ok(user);
    }
}