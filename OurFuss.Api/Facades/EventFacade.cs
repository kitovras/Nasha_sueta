using OurFuss.Api.Converters;
using OurFuss.Api.Converters.Dto.Activities.Request;
using OurFuss.Api.Converters.Dto.Activities.Response;
using OurFuss.Api.Infrastructure.Models.ApiResponse;
using OurFuss.Core.Modules.Activities.Domains;
using OurFuss.Core.Modules.Activities.Services;

namespace OurFuss.Api.Facades;

/// <inheritdoc/>
public class EventFacade : IEventFacade
{
    /// <summary>
    /// Репозиторий событий
    /// </summary>
    private readonly IEventService _eventService;

    public EventFacade(IEventService eventService)
    {
        _eventService = eventService;
    }

    /// <inheritdoc/>
    public async Task<Result> AddEventsAsync(ApiAddEventRequest request)
    {
        //TODO: Валидация

        var events = EventConverter.MapApiEventToEventDomain().Map<List<Event>>(request.Events);
        await _eventService.AddEventsAsync(events);

        return new();
    }

    /// <inheritdoc/>
    public async Task<Result> AddEventTagsAsync(ApiAddEventTagRequest request)
    {
        //TODO: Валидация

        var eventTags = request.EventTags.Distinct();
        var eventTagDomains = EventConverter.MapApiEventTagToDomainEventTag().Map<List<EventTag>>(eventTags);

        await _eventService.AddEventTagsAsync(eventTagDomains);

        return new();
    }

    /// <inheritdoc/>
    public async Task<Result<ApiGetEventsResponse>> GetEventsByFilterAsync(DateTime currentDateTime)
    {
        //TODO: Валидация

        var events = await _eventService.GetEventsByFilterAsync(currentDateTime);
        var apiEvents = EventConverter.MapEventDomainToApiEvent().Map<List<ApiEventByFilterResponse>>(events);

        var apiGetEventsResponse = new ApiGetEventsResponse
        {
            Events = apiEvents,
        };

        return new(apiGetEventsResponse);
    }
}