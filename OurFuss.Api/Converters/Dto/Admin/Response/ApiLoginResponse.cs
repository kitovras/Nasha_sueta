namespace OurFuss.Api.Converters.Dto.Admin.Response;

/// <summary>
/// Api-ответ на авторизацию
/// </summary>
public class ApiLoginResponse
{
    /// <summary>
    /// Jwt-токен доступа
    /// </summary>
    public string JwtToken { get; set; } = string.Empty;
}
