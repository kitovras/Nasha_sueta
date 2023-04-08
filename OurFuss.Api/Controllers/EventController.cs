using Microsoft.AspNetCore.Mvc;
using OurFuss.Api.Converters;
using OurFuss.Api.Facades;

namespace OurFuss.Controllers;

/// <summary>
/// Контроллер событий
/// </summary>
[ApiController]
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
    /// Получить все события
    /// </summary>
    /// <returns>Коллекция событий</returns>
    [HttpGet(nameof(GetAllEvents))]
    public async Task<ActionResult<List<ApiEvent>>> GetAllEvents()
    {
        var apiEvents = await _eventFacade.GetEventAllAsync();
        return Ok(apiEvents);
    }
}