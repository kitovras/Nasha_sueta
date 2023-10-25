using OurFuss.Core.Modules.Activities.Domains;
using OurFuss.Core.Modules.Activities.Repositories;

namespace OurFuss.Core.Modules.Activities.Services;

/// <inheritdoc/>
public class EventService : IEventService
{
    /// <summary>
    /// Репозиторий событий
    /// </summary>
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /// <inheritdoc/>
    public async Task AddEventsAsync(List<Event> events)
    {
        //TODO: Валидация

        await _eventRepository.AddEventsAsync(events);
    }

    /// <inheritdoc/>
    public async Task AddEventTagsAsync(List<EventTag> eventTags)
    {
        //TODO: Валидация

        await _eventRepository.AddEventTagsAsync(eventTags);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Event>> GetEventsByFilterAsync(DateTime currentDateTime)
    {
        return await _eventRepository.GetEventsByFilterAsync(currentDateTime);
    }
}