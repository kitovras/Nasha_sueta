using Microsoft.EntityFrameworkCore;
using OurFuss.Core.Sections.Entities.Models;
using OurFuss.Core.Sections.Telegram.Enums;
using OurFuss.Core.Sections.Telegram.Models.Entities;

namespace OurFuss.Core.Sections.Telegram.Extensions.Queryable;

internal static class TelegramQueryExtensions
{
    /// <summary>
    /// Get a user by Chat ID Asynchronously
    /// </summary>
    /// <param name="dbContextDetails">Information about the database context</param>
    /// <param name="chatId">Chat ID</param>
    /// <returns>Telegram user</returns>
    public static async Task<TelegramAccountEntity?> GetUserByChatIdAsync(
        this DbContextDetails<TelegramAccountEntity> dbContextDetails, long chatId)
    {
        var telegramUser = await dbContextDetails.Queryable.FirstOrDefaultAsync(foda => foda.ChatId == chatId);

        dbContextDetails.ContextDestroy(dbContextDetails.DbContextId);
        return telegramUser;
    }

    /// <summary>
    /// Get users by roles
    /// </summary>
    /// <param name="dbContextDetails">Information about the database context</param>
    /// <param name="role">Role type</param>
    /// <returns>Telegram users</returns>
    public static async Task<List<TelegramAccountEntity>> GetUsersByRoleAsync(
        this DbContextDetails<TelegramAccountEntity> dbContextDetails, TelegramRoleType role)
    {
        var telegramUsers = await dbContextDetails.Queryable.Where(foda => foda.TelegramRole == role).ToListAsync();

        dbContextDetails.ContextDestroy(dbContextDetails.DbContextId);
        return telegramUsers;
    }

    /// <summary>
    /// User exist
    /// </summary>
    /// <param name="dbContextDetails">Information about the database context</param>
    /// <param name="chatId">Chat ID</param>
    /// <returns>True - user exist, else - false</returns>
    public static async Task<bool> UserExistAsync(
        this DbContextDetails<TelegramAccountEntity> dbContextDetails, long chatId)
    {
        var telegramUserExist = await dbContextDetails.Queryable.AnyAsync(aa => aa.ChatId == chatId);

        dbContextDetails.ContextDestroy(dbContextDetails.DbContextId);
        return telegramUserExist;
    }
}
