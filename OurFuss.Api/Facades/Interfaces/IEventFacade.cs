using OurFuss.Api.Converters;

namespace OurFuss.Api.Facades;

/// <summary>
/// Фасад событий
/// </summary>
public interface IEventFacade
{
    /// <summary>
    /// Асинхронно получить все события
    /// </summary>
    /// <returns>Коллекция событий</returns>
    Task<List<ApiEvent>> GetEventAllAsync();
}