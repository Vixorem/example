using Example.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.DAL;

public class ApplicationContext : DbContext
{
    /// <summary>
    /// Поле юзеров. Через него происходит обращение к таблице юзеров в БД
    /// </summary>
    public DbSet<UserModel> Users { get; set; }
    
    /// <summary>
    /// Это конструктор. Такой параметр нужен, если мы не хотим писать строку
    /// с адресом и паролем к нашей БД здесь
    /// </summary>
    /// <param name="options"></param>
    public ApplicationContext(DbContextOptions options): base(options) {}

    /// <summary>
    /// Конфигурация сущностей в таблицах
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // В этом методе можно вставлять ограничения или индексы для столбцов БД.
        // Для примера, мы навесили индекс на столбец логина
        modelBuilder.Entity<UserModel>().HasIndex(model => model.Login);
    }
}