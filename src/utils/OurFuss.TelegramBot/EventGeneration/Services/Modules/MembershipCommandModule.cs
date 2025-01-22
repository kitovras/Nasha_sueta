using Microsoft.Extensions.Logging;
using OurFuss.Core.Sections.Telegram.Enums;
using OurFuss.Core.Sections.Telegram.Services;
using OurFuss.TelegramBot.EventGeneration.Enums;
using OurFuss.TelegramBot.EventGeneration.Parameters;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OurFuss.TelegramBot.EventGeneration.Services.Modules;

/// <summary>
/// Membership management module
/// </summary>
public class MembershipCommandModule : ITelegramCommandModule
{
    /// <inheritdoc/>
    public TelegramCommandType TelegramCommand => TelegramCommandType.Membership;

    /// <summary>
    /// Logger
    /// </summary>
    private readonly ILogger<MembershipCommandModule> _logger;

    /// <summary>
    /// A client to use the Telegram Bot API
    /// </summary>
    private readonly TelegramBotClient _telegramBotClient;

    private readonly ITelegramUserService _telegramUserService;

    public MembershipCommandModule(
        ITelegramClientFactory telegramBotClientFactory,
        ITelegramUserService telegramUserService,
        ILogger<MembershipCommandModule> logger)
    {
        _telegramUserService = telegramUserService;
        _telegramBotClient = telegramBotClientFactory.GetClientAsync().Result;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task ExecuteAsync(Update update)
    {
        var myChatMember = update.MyChatMember;
        var chatId = myChatMember.Chat.Id;
        
        try
        {
            var userMemberStatus = GetChatMemberStatus(myChatMember.NewChatMember.Status);
            await _telegramUserService.SetUserMemberStatusAsync(chatId, userMemberStatus);
        }
        catch (Exception exception)
        {
            var logMessage =
                $"Error in method {nameof(ExecuteAsync)} and module {TelegramCommand}. " +
                $"Message: {exception.Message}, stackTrace: {exception.StackTrace}";

            _logger.LogCritical(logMessage);

            await _telegramBotClient.SendMessage(chatId, MessageParameters.TechnicalErrorMessage, parseMode: ParseMode.Html);
        }
    }

    #region Helpers

    /// <summary>
    /// Get the status of a chat participant
    /// </summary>
    /// <param name="chatMemberStatus">Chat participant status</param>
    /// <returns>Member status of the user</returns>
    private static UserMemberStatus GetChatMemberStatus(ChatMemberStatus chatMemberStatus)
    {
        return chatMemberStatus switch
        {
            ChatMemberStatus.Kicked => UserMemberStatus.Kicked,
            ChatMemberStatus.Member => UserMemberStatus.Member,
            ChatMemberStatus.Creator or ChatMemberStatus.Administrator or ChatMemberStatus.Left or ChatMemberStatus.Restricted => UserMemberStatus.Unknown,
            _ => UserMemberStatus.Unknown,
        };
    }

    #endregion
}

