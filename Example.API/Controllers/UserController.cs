using Example.API.Views;
using Microsoft.AspNetCore.Mvc;

namespace Example.API.Controllers;

/// <summary>
/// Контроллер для работы с пользователем
/// </summary>
[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly UserService _service;
    public UserController(UserService service)
    {
        _service = service;
    }

    /// <summary>
    /// Поиск юзера по логину
    /// </summary>
    /// <param name="login">Логин</param>
    [HttpGet("user")] // атрибут для гет запроса
    public ActionResult<UserSearchView> GetUserByLogin(string login)
    {
        
        if (login == string.Empty)
            return Problem(statusCode: 404, detail: "Не указан логин");
        
        var userRes = _service.GetUserByLogin(login);
        if (userRes.IsFailure)
            return Problem(statusCode: 404, detail: userRes.Error);

        return Ok(new UserSearchView
        {
            Id = userRes.Value.Id,
            Login = userRes.Value.Login
        });
    }
}