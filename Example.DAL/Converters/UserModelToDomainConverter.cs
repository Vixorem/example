using Example.DAL.Models;

namespace Example.DAL.Converters;

/// <summary>
///  Класс конвертер
/// </summary>
/// <remarks>
/// Этот класс называется extension класс,
/// поэтому его нужно сделать статик
/// Добавив слово this мы сделал экстенш метод,
/// теперь мы сможем писать model.ToDomain,
/// как будто у объекта UserModel
/// есть такой метод, но на самом деле его нет,
/// он живет в отдельном статик классе
/// </remarks>
public static class UserModelToDomainConverter
{
    public static User? ToDomain(this UserModel model)
    {
        return new User
        {
            Id = model.Id,
            Login = model.Login
        };
    }
}