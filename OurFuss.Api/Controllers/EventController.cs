using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurFuss.Api.Converters.Dto.Activities.Request;
using OurFuss.Api.Converters.Dto.Activities.Response;
using OurFuss.Api.Facades;
using OurFuss.Api.Infrastructure.Models.ApiResponse;

namespace OurFuss.Controllers;

/// <summary>
/// Контроллер событий
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "viewer")]
public class EventController : Controller
{
    /// <summary>
    /// Фасад событий
    /// </summary>
    private readonly IEventFacade _eventFacade;

    public EventController(IEventFacade eventFacade)
    {
        _eventFacade = eventFacade;
    }

    /// <summary>
    /// Асинхронно получить события по фильтру
    /// </summary>
    /// <param name="request">Api-запрос на получение событий</param>
    /// <returns>Api-ответ на получение событий</returns>
    [HttpGet(nameof(GetEventsByFilter))]
    public async Task<Result<ApiGetEventsResponse>> GetEventsByFilter([FromQuery] ApiGetEventsRequest request) =>
        await _eventFacade.GetEventsByFilterAsync(request.CurrentDateTime);
}