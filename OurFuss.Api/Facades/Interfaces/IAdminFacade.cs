using OurFuss.Api.Converters.Dto.Admin.Request;
using OurFuss.Api.Converters.Dto.Admin.Response;
using OurFuss.Api.Infrastructure.Models.ApiResponse;

namespace OurFuss.Api.Facades;

/// <summary>
/// Фасад администрирования
/// </summary>
public interface IAdminFacade
{
    /// <summary>
    /// Асинхронно авторизоваться
    /// </summary>
    /// <param name="request">Api-запрос на авторизацию</param>
    /// <returns>Api-ответ на авторизацию</returns>
    Task<Result<ApiLoginResponse>> LoginAsync(ApiLoginRequest request);
}