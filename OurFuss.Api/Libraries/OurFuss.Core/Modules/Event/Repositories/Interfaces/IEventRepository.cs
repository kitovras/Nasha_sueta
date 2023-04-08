namespace OurFuss.Core.Modules.Event.Repositories;

/// <summary>
/// Репозиторий событий
/// </summary>
public interface IEventRepository
{
    /// <summary>
    /// Асинхронно получить все события
    /// </summary>
    /// <returns>Коллекция событий</returns>
    Task<List<Domains.Event>> GetEventAllAsync();
}