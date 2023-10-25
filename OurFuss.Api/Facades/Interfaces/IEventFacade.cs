using OurFuss.Api.Converters.Dto.Activities.Request;
using OurFuss.Api.Converters.Dto.Activities.Response;
using OurFuss.Api.Infrastructure.Models.ApiResponse;

namespace OurFuss.Api.Facades;

/// <summary>
/// Фасад событий
/// </summary>
public interface IEventFacade
{
    /// <summary>
    /// Асинхронно добавить события
    /// </summary>
    /// <param name="request">Api-запрос на добавление события</param>
    /// <returns>Http-ответ</returns>
    Task<Result> AddEventsAsync(ApiAddEventRequest request);

    /// <summary>
    /// Асинхронно добавить тэги события
    /// </summary>
    /// <param name="request">Api-запрос на добавление события</param>
    /// <returns>Http-ответ</returns>
    Task<Result> AddEventTagsAsync(ApiAddEventTagRequest request);

    /// <summary>
    /// Асинхронно получить события по фильтру
    /// </summary>
    /// <param name="currentDateTime">Текущая дата и время</param>
    /// <returns>Api-ответ на получение событий</returns>
    Task<Result<ApiGetEventsResponse>> GetEventsByFilterAsync(DateTime currentDateTime);
}