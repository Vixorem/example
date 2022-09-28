using Example;

namespace UnitTests;

public class UserTests
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UserTests()
    {
        // Используем библиотеку Moq, чтобы подготавливать тестовые данные
        // Мы отдаем реализацию интерфейса, но сервису без разницы, что там, ему важно, что удовлетворяется интерфейсу
        // Таким образом мы подкидываем нужные данные для тестовых сценариев, другими словами, "мокаем" (mock) репозиторий 
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public void LoginIsEmptyOrNull_ShouldFail()
    {
        var res = _userService.GetUserByLogin(string.Empty);
        
        Assert.True(res.IsFailure); // Ожидаем, что вернет ошибку
        Assert.Equal("Логин не был указан", res.Error); // убеждаемся, что ошибка именно та
    }
    
    [Fact]
    public void UserNotFound_ShouldFail()
    {
        // It.IsAny означает, что мы подготавливаем то, что должен вернуть метод вне зависимости от того, какой логин мы указали
        // То есть в данном случае, можно скормить любой string, так как мы тестируем сценарий ненахода, нам в любом случае надо вернуть нуль
        _userRepositoryMock.Setup(repository => repository.GetByLogin(It.IsAny<string>()))
            .Returns(() => null); 
        
        var res = _userService.GetUserByLogin("qwertyuiop"); // что угодно в виде строки
        
        Assert.True(res.IsFailure); // Ожидаем, что вернет ошибку
        Assert.Equal("Пользователь не найден", res.Error); // убеждаемся, что ошибка именно та
    }
    
    [Fact]
    public void LoginCorrect_ShouldFindUser()
    {
        var login = "qwerty";
        _userRepositoryMock.Setup(repository => repository.GetByLogin(login))
            .Returns(() => new User
            {
                Login = login
            }); 
        
        var res = _userService.GetUserByLogin(login);
        
        Assert.True(res.Success); // Ожидаем, что вернет ошибки нет
        Assert.Equal(login, res.Value.Login); // убеждаемся, что юзер тот что надо
    }
}