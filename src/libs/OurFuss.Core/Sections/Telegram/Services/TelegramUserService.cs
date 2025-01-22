using System.Threading.Tasks;
using OurFuss.Core.Sections.Entities;
using OurFuss.Core.Sections.Telegram.Enums;
using OurFuss.Core.Sections.Telegram.Extensions.Queryable;
using OurFuss.Core.Sections.Telegram.Models.Entities;
using OurFuss.Core.Utils.Guid;

namespace OurFuss.Core.Sections.Telegram.Services;

/// <inheritdoc/>
public class TelegramUserService : ITelegramUserService
{
    private readonly IEntityRepository _entityRepository;
    private readonly IGuidGenerator _guidGenerator;

    public TelegramUserService(
        IEntityRepository entityRepository,
        IGuidGenerator guidGenerator)
    {
        _entityRepository = entityRepository;
        _guidGenerator = guidGenerator;
    }

    /// <inheritdoc/>
    public async Task<TelegramAccountEntity> AddAccountDefaultAsync(string fullName, long chatId)
    {
        var telegramUserDefault = GetTelegramUserDefault(fullName, chatId);

        await _entityRepository.AddAsync(telegramUserDefault);
        return telegramUserDefault;
    }

    /// <inheritdoc/>
    public async Task<TelegramAccountEntity?> GetAccountByChatIdAsync(long chatId) =>
        await _entityRepository.GetContextDetails<TelegramAccountEntity>().GetUserByChatIdAsync(chatId);

    /// <inheritdoc/>
    public async Task<List<TelegramAccountEntity>> GetAdminsAsync() =>
        await _entityRepository.GetContextDetails<TelegramAccountEntity>().GetUsersByRoleAsync(TelegramRoleType.Admin);

    /// <inheritdoc/>
    public async Task SetUserMemberStatusAsync(long chatId, UserMemberStatus userMemberStatus)
    {
        var user = await GetAccountByChatIdAsync(chatId);
        if (user is null)
            return;

        user.UserMemberStatus = userMemberStatus;
        user.Modified = DateTime.UtcNow;

        await _entityRepository.UpdateAsync(user);
    }

    /// <inheritdoc/>
    public async Task<bool> UserExistAsync(long chatId) =>
        await _entityRepository.GetContextDetails<TelegramAccountEntity>().UserExistAsync(chatId);

    #region Helpers

    private TelegramAccountEntity GetTelegramUserDefault(string fullName, long chatId)
    {
        var telegramAccount = new TelegramAccountEntity
        {
            Id = _guidGenerator.Sequential(),
            ChatId = chatId,
            FullName = fullName,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            TelegramRole = TelegramRoleType.User,
            UserMemberStatus = UserMemberStatus.Member,
        };

        return telegramAccount;
    }

    #endregion
}
