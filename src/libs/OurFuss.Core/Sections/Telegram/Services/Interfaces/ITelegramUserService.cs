using OurFuss.Core.Sections.Telegram.Enums;
using OurFuss.Core.Sections.Telegram.Models.Entities;

namespace OurFuss.Core.Sections.Telegram.Services;

public interface ITelegramUserService
{
    Task<TelegramAccountEntity?> GetAccountByChatIdAsync(long chatId);
    Task<TelegramAccountEntity> AddAccountDefaultAsync(string fullName, long chatId);
    Task SetUserMemberStatusAsync(long chatId, UserMemberStatus userMemberStatus);
    Task<List<TelegramAccountEntity>> GetAdminsAsync();
    
    Task<bool> UserExistAsync(long chatId);
}
