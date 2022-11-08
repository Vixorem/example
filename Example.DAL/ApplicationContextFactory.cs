using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Example.DAL;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    /// <summary>
    /// Создание контекста (требуется nuget
    /// Microsoft.EntityFrameworkCore.Design
    /// и + Npgsql.EntityFrameworkCore.PostgreSQL.Design,
    /// если постгрес или в зависимости от вашей СУБД)
    /// </summary>
    /// <remarks>
    /// Этот класс и метод используются только
    /// при работе с миграциями.
    /// То есть, когда мы урками в консоле пишем
    /// команды для создания миграций и их выполнению,
    /// запускается этот класс и создает контекст, чтобы
    /// Ef понимал, в какой бд и какие миграции нужно выполнить.
    ///
    /// Обычно, тут не нужно хранить пароль прям в коде,
    /// лично я делаю так:
    /// 0) Тут строка подключения всегда пустая
    /// 1) Когда мне надо поработать с миграциями, я
    /// заполняю строку подключения
    /// 2) Выполняю команды для миграций
    /// 3) Они выполняются
    /// 4) Я удаляю строку подключения, чтоб не
    /// коммитить это в репу
    /// </remarks>
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseNpgsql(
            $"Host=localhost;Port=5432;Database=exampleDB;Username=exampleUser;Password=examplePswd");

        return new ApplicationContext(optionsBuilder.Options);
    }
}