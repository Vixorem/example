﻿using System.Text.Json.Serialization;

namespace Example.API.Views;

/// <summary>
/// Вьюха для ответа на запрос получения пользователя
/// </summary>
/// <remarks>
/// Атрибут JsonPropertyName нужен для сериализации или десериализации сообщений в формате джсон
/// Простым словами, эти атрибуты помогают сконвертить класс в джсон с указанными в них названиями полей
/// И, наоборот, десериализовать/распарсить джсон обратно в объект, в соответствии с названиями полей
/// </remarks>
public class UserSearchView
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    [JsonPropertyName("login")]
    public string Login { get; set; }
}