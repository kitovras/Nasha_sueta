using Microsoft.AspNetCore.Identity;
using OurFuss.Core.Db.Entities.User;
using Wheat.Api.Infrastructure.Extensions;

namespace OurFuss.Api.Infrastructure;

/// <summary>
/// Генератор jwt-токенов
/// </summary>
public sealed class GwtTokenGenerator
{
    /// <summary>
    /// Конфигурации приложения
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Менеджер входа в систему
    /// </summary>
    private readonly SignInManager<UserEntity> _signInManager;

    public GwtTokenGenerator(IConfiguration configuration, SignInManager<UserEntity> signInManager)
    {
        _configuration = configuration;
        _signInManager = signInManager;
    }

    /// <summary>
    /// Получить токен
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns>Jwt-токен</returns>
    internal string GetToken(UserEntity user) => _signInManager.GenerateJWTAutorizeToken(_configuration, user);
}