using Example;
using Example.DAL;
using Example.DAL.Models;
using Example.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Database;

/// <summary>
/// ВНИМАНИЕ! Это не тест в адекватном смысле. Мы засунули этот класс в проект теста,
/// чтобы поиграться с ApplicationContext и посмотреть, нормально ли работает подключение к бд.
///
/// СОВЕТУЮ это все либо закомментить, либо удалить, когда наиграетесь, чтобы при запуске всех тестов,
/// чтобы эта фигня не зааффектила вашу бд. Или можете воздать вторую (тестовую) бд для таких делишек
/// </summary>
public class EfPlayground
{
    private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;

    public EfPlayground()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql(
            $"Host=localhost;Port=5432;Database=exampleDB;Username=exampleUser;Password=examplePswd");
        _optionsBuilder = optionsBuilder;
    }

    /// <summary>
    /// Просто реально добавили запись в БД и проверили, добавилось ли
    /// </summary>
    [Fact]
    public void PlaygroundMethod1()
    {
        using var context = new ApplicationContext(_optionsBuilder.Options);
        context.Users.Add(new UserModel
        {
            Id = 123,
            Login = "TEST"
        });
        context.SaveChanges(); // сохранили в БД

        Assert.True(context.Users.Any(u => u.Login == "TEST")); // проверим, нашло ли в нашей бд

        // Можно например написать такой тест, где мы просто сохранили запись,
        // а потом пойти руками в СУБД посмотреть и убедиться самим, что она там есть 
    }

    /// <summary>
    /// Просто реально удалили запись в БД и проверили, удалилось ли
    /// </summary>
    [Fact]
    public void PlaygroundMethod2()
    {
        using var context = new ApplicationContext(_optionsBuilder.Options);
        var u = context.Users.FirstOrDefault(u => u.Login == "TEST");
        context.Users.Remove(u);
        context.SaveChanges(); // удалили в БД

        Assert.True(!context.Users.Any(u => u.Login == "TEST"));
    }

    /// <summary>
    /// А вот тут можно приблизительно показать, как у нас будет работать реальный код
    /// </summary>
    [Fact]
    public void PlaygroundMethod3()
    {
        # region подготовили сервис

        using var context = new ApplicationContext(_optionsBuilder.Options);
        var userRepository = new UserRepository(context);
        var userService = new UserService(userRepository);

        # endregion

        // Подгтовили сервис, которому дали репозиторий, который юзает контекст
        var res = userService.GetUserByLogin("TEST");
        
        Assert.NotNull(res.Value);
    }
}