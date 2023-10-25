using Microsoft.EntityFrameworkCore;
using OurFuss.Core.Db;
using OurFuss.Core.Db.Entities.Event;
using OurFuss.Core.Modules.Activities.Domains;

namespace OurFuss.Core.Modules.Activities.Repositories;

/// <inheritdoc/>
public class EventRepository : IEventRepository
{
    /// <summary>
    /// Фабрика контекста базы данных
    /// </summary>
    private readonly IDbContextFactory<OurFussDbContext> _dbContextFactory;

    public EventRepository(IDbContextFactory<OurFussDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    /// <inheritdoc/>
    public async Task AddEventsAsync(List<Event> events)
    {
        await using var dbContext = _dbContextFactory.CreateDbContext();

        var eventEntities = new List<EventEntity>();
        foreach (var eventDomain in events)
        {
            var eventEntity = ActivityMapper.MapEventDomainToEventEntity().Map<EventEntity>(eventDomain);
            
            if (!eventDomain.EventTags.Any())
            {
                eventEntities.Add(eventEntity);
                continue;
            }

            var tagEntities = ActivityMapper.MapTagDomainToTagEntity().Map<IEnumerable<TagEntity>>(eventDomain.EventTags);

            var eventTagEntities = new List<EventTagEntity>();
            foreach (var tagEntity in tagEntities)
            {
                var eventTagEntity = new EventTagEntity
                {
                    Event = eventEntity,
                    Tag = tagEntity
                };

                eventTagEntities.Add(eventTagEntity);
            }

            eventEntity.EventTags!.AddRange(eventTagEntities);            
        }

        dbContext.AddRange(eventEntities);
        await dbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task AddEventTagsAsync(List<EventTag> eventTags)
    {
        await using var dbContext = _dbContextFactory.CreateDbContext();

        var eventTagEntities = ActivityMapper.MapTagDomainToTagEntity().Map<List<EventTagEntity>>(eventTags);

        await dbContext.EventTag.AddRangeAsync(eventTagEntities);
        await dbContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Event>> GetEventsByFilterAsync(DateTime dateTime)
    {
        await using var dbContext = _dbContextFactory.CreateDbContext();

        var eventEntities = await dbContext.Events.AsNoTracking()
            .Include(i => i.EventTags!)
                .ThenInclude(ti => ti.Tag)
            .Where(w => w.StartDate.Date <= dateTime.Date || w.StartDate.Date >= dateTime.Date)
            .ToListAsync();

        var events = new List<Event>();
        foreach (var eventEntity in eventEntities)
        {
            var eventDomain = ActivityMapper.MapEventEntityToEventDomain().Map<Event>(eventEntity);

            var eventTags = new List<EventTag>();
            if (eventEntity.EventTags is not null)
            {
                var tags = eventEntity.EventTags.Select(s => s.Tag);
                eventTags = ActivityMapper.MapTagEntityToTagDomain().Map<List<EventTag>>(tags);
            }

            eventDomain.EventTags = eventTags;
            events.Add(eventDomain);
        }

        return events;
    }
}