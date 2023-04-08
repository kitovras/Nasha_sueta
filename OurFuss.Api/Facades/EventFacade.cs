using OurFuss.Api.Converters;
using OurFuss.Core.Modules.Event.Repositories;

namespace OurFuss.Api.Facades;

/// <inheritdoc/>
public class EventFacade : IEventFacade
{
    /// <summary>
    /// Репозиторий событий
    /// </summary>
    private readonly IEventRepository _eventRepository;

    public EventFacade(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /// <inheritdoc/>
    public async Task<List<ApiEvent>> GetEventAllAsync()
    {
        var events = await _eventRepository.GetEventAllAsync();
        return EventConverter.EventConverted(events);
    }
}