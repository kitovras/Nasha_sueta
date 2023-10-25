namespace OurFuss.Api.Converters.Dto.Admin.Request;

/// <summary>
/// Api-запрос на авторизацию
/// </summary>
public class ApiLoginRequest
{
    /// <summary>
    /// Наименование пользователя
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
