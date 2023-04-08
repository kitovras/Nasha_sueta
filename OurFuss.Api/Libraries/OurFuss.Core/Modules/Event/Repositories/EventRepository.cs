using Microsoft.EntityFrameworkCore;
using OurFuss.Core.Db;
using OurFuss.Core.Modules.Event.Domains;

namespace OurFuss.Core.Modules.Event.Repositories;

/// <inheritdoc/>
public class EventRepository : IEventRepository
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    private readonly OurFussDbContext _dbContext;

    public EventRepository(OurFussDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<List<Domains.Event>> GetEventAllAsync()
    {
        var events = await _dbContext.Events.AsNoTracking().ToListAsync();
        return EventMapper.MapEvent().Map<List<Domains.Event>>(events);
    }
}