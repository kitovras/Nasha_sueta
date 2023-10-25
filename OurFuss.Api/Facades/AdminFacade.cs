using Microsoft.AspNetCore.Identity;
using OurFuss.Api.Converters.Dto.Admin.Request;
using OurFuss.Api.Converters.Dto.Admin.Response;
using OurFuss.Api.Infrastructure;
using OurFuss.Api.Infrastructure.Models.ApiResponse;
using OurFuss.Core.Db.Entities.User;

namespace OurFuss.Api.Facades;

/// <inheritdoc/>
public class AdminFacade : IAdminFacade
{
    /// <summary>
    /// Менеджер входа в систему
    /// </summary>
    private readonly SignInManager<UserEntity> _signInManager;

    /// <summary>
    /// Менеджер пользователей
    /// </summary>
    private readonly UserManager<UserEntity> _userManager;

    /// <summary>
    /// Генератор jwt-токенов
    /// </summary>
    private readonly GwtTokenGenerator _gwtTokenGenerator;

    public AdminFacade(
        SignInManager<UserEntity> signInManager,
        UserManager<UserEntity> userManager,
        GwtTokenGenerator gwtTokenGenerator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _gwtTokenGenerator = gwtTokenGenerator;
    }

    /// <inheritdoc/>
    public async Task<Result<ApiLoginResponse>> LoginAsync(ApiLoginRequest request)
    {
        //TODO:Валидация

        var user = await _userManager.FindByNameAsync(request.UserName);
        var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);

        if (signInResult.Succeeded == false)
            return new(StatusError.UserWithThisNameNotExist);

        var token = _gwtTokenGenerator.GetToken(user);
        var apiLoginResponse = new ApiLoginResponse
        {
            JwtToken = token,
        };

        return new(apiLoginResponse);
    }
}