using OurFuss.Core.Modules.Activities.Domains;

namespace OurFuss.Core.Modules.Activities.Repositories;

/// <summary>
/// Репозиторий событий
/// </summary>
public interface IEventRepository
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
    /// <param name="dateTime">Текущая дата и время</param>
    /// <returns>Коллекция событий</returns>
    Task<IEnumerable<Event>> GetEventsByFilterAsync(DateTime dateTime);
}