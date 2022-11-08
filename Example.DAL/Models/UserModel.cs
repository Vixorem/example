namespace Example.DAL.Models;

/// <summary>
/// Класс, который на самом деле таблица юзеров в БД
/// </summary>
public class UserModel
{
    public int Id { get; set; }
    public string Login { get; set; }
}