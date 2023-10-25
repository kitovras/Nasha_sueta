using OurFuss.Core.Modules.Activities.Domains;

namespace OurFuss.Core.Modules.Activities.Services;

/// <summary>
/// Сервис событий
/// </summary>
public interface IEventService
{
    /// <summary>
    /// Асинхронно добавить события
    /// </summary>
    /// <param name="events">События</param>
    /// <returns>Результат асинхронной задачи</returns>
    Task AddEventsAsync(List<Event> events);

    /// <summary>
    /// Асинхронно добавить тэги события
    /// </summary>
    /// <param name="eventTags">Тэги события</param>
    /// <returns>Результат асинхронной задачи</returns>
    Task AddEventTagsAsync(List<EventTag> eventTags);

    /// <summary>
    /// Асинхронно получить события по фильтру
    /// </summary>
    /// <param name="currentDateTime">Текущая дата и время</param>
    /// <returns>Коллекция событий</returns>
    Task<IEnumerable<Event>> GetEventsByFilterAsync(DateTime currentDateTime);
}