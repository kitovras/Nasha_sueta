using Microsoft.EntityFrameworkCore;
using OurFuss.Core.Sections.Entities.Models;
using OurFuss.Core.Sections.Events.Enums;
using OurFuss.Core.Sections.Events.Models.Domains;
using OurFuss.Core.Sections.Events.Models.Entities;

namespace OurFuss.Core.Sections.Events.Extensions.Queryable;

internal static class EventQueryExtensions
{
    /// <summary>
    /// Asynchronously receive an extended user event with telegram account
    /// </summary>
    /// <param name="dbContextDetails">Information about the database context</param>
    /// <param name="userEventCustomId">User event custom ID</param>
    /// <returns>User event custom</returns>
    public static async Task<UserEventCustomEntity?> GetUserEventCustomWithTelegramAccountAsync(
        this DbContextDetails<UserEventCustomEntity> dbContextDetails, Guid userEventCustomId)
    {
        var userEventCustom = await dbContextDetails.Queryable.AsNoTracking()
            .Include(i => i.TelegramAccount)
            .FirstOrDefaultAsync(foda => foda.Id == userEventCustomId);

        dbContextDetails.ContextDestroy(dbContextDetails.DbContextId);
        return userEventCustom;
    }

    /// <summary>
    /// Asynchronously receive an extended user event
    /// </summary>
    /// <param name="dbContextDetails">Information about the database context</param>
    /// <param name="userEventCustomId">User event custom ID</param>
    /// <returns>User event custom</returns>
    public static async Task<UserEventCustomEntity?> GetUserEventCustomAsync(
        this DbContextDetails<UserEventCustomEntity> dbContextDetails, Guid userEventCustomId)
    {
        var userEventCustom = await dbContextDetails.Queryable.AsNoTracking()
            .FirstOrDefaultAsync(foda => foda.Id == userEventCustomId);

        dbContextDetails.ContextDestroy(dbContextDetails.DbContextId);
        return userEventCustom;
    }

    public static async Task<List<UserEventCustomEntity>> GetUserEventFullAsync(
        this DbContextDetails<UserEventCustomEntity> dbContextDetails, int skip, int take, EventStatusType eventStatus)
    {
        var userEventCustom = await dbContextDetails.Queryable.AsNoTracking()
            .Include(i => i.TelegramAccount)
            .Include(i => i.Photos)
            .Where(w => w.EventStatus == eventStatus)
            .OrderBy(ob => ob.Created)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        dbContextDetails.ContextDestroy(dbContextDetails.DbContextId);
        return userEventCustom;
    }

    public static async Task<int> GetEventCountAsync(
        this DbContextDetails<UserEventCustomEntity> dbContextDetails, EventStatusType eventStatus)
    {
        var userEventCustomCount = await dbContextDetails.Queryable.AsNoTracking()
            .CountAsync(ca => ca.EventStatus == eventStatus);

        dbContextDetails.ContextDestroy(dbContextDetails.DbContextId);
        return userEventCustomCount;
    }

    /// <summary>
    /// Get a complete picture of the user event
    /// </summary>
    /// <param name="dbContextDetails">Information about the database context</param>
    /// <param name="userEventCustomId">User event custom ID</param>
    /// <returns>User event full</returns>
    public static async Task<UserEventCustomEntity?> GetUserEventFullAsync(
        this DbContextDetails<UserEventCustomEntity> dbContextDetails, Guid userEventCustomId)
    {
        var userEventCustom = await dbContextDetails.Queryable.AsNoTracking()
            .Include(i => i.TelegramAccount)
            .Include(i => i.Photos)
            .FirstOrDefaultAsync(foda => foda.Id == userEventCustomId);

        dbContextDetails.ContextDestroy(dbContextDetails.DbContextId);
        return userEventCustom;
    }
}
