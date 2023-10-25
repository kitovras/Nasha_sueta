using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurFuss.Api.Converters.Dto.Activities.Request;
using OurFuss.Api.Converters.Dto.Admin.Request;
using OurFuss.Api.Converters.Dto.Admin.Response;
using OurFuss.Api.Facades;
using OurFuss.Api.Infrastructure.Models.ApiResponse;

namespace OurFuss.Api.Controllers;

/// <summary>
/// Контроллер администратора
/// </summary>
[ApiController]
public class AdminController : Controller
{
    /// <summary>
    /// Фасад событий
    /// </summary>
    private readonly IEventFacade _eventFacade;

    /// <summary>
    /// Фасад администрирования
    /// </summary>
    private readonly IAdminFacade _adminFacade;

    public AdminController(IEventFacade eventFacade, IAdminFacade adminFacade)
    {
        _eventFacade = eventFacade;
        _adminFacade = adminFacade;
    }

    /// <summary>
    /// Асинхронно авторизоваться
    /// </summary>
    /// <param name="request">Api-запрос на авторизацию</param>
    /// <returns>Api-ответ на авторизацию</returns>
    [HttpGet(nameof(Login))]
    public async Task<Result<ApiLoginResponse>> Login([FromQuery] ApiLoginRequest request) => 
        await _adminFacade.LoginAsync(request);

    /// <summary>
    /// Асинхронно добавить события
    /// </summary>
    /// <param name="request">Api-запрос на добавление событий</param>
    /// <returns>Http-ответ</returns>
    [HttpPost(nameof(AddEvents))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public async Task<Result> AddEvents([FromBody] ApiAddEventRequest request) =>
        await _eventFacade.AddEventsAsync(request);

    /// <summary>
    /// Асинхронно добавить тэги события
    /// </summary>
    /// <param name="request">Api-запрос на добавление тэгов события</param>
    /// <returns>Http-ответ</returns>
    [HttpPost(nameof(AddEventTags))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public async Task<Result> AddEventTags([FromBody] ApiAddEventTagRequest request) =>
        await _eventFacade.AddEventTagsAsync(request);
}
