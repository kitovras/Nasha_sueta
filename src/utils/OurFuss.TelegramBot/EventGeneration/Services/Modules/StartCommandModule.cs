using Microsoft.Extensions.Logging;
using OurFuss.Core.Sections.Telegram.Services;
using OurFuss.TelegramBot.EventGeneration.Enums;
using OurFuss.TelegramBot.EventGeneration.Parameters;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OurFuss.TelegramBot.EventGeneration.Services.Modules;

/// <summary>
/// Start command handler
/// </summary>
public class StartCommandModule : ITelegramCommandModule
{
    /// <inheritdoc/>
    public TelegramCommandType TelegramCommand => TelegramCommandType.Start;

    /// <summary>
    /// Logger
    /// </summary>
    private readonly ILogger<StartCommandModule> _logger;

    /// <summary>
    /// A client to use the Telegram Bot API
    /// </summary>
    private readonly TelegramBotClient _telegramBotClient;

    private readonly ITelegramUserService _telegramUserService;

    public StartCommandModule(
        ITelegramClientFactory telegramBotClientFactory,
        ITelegramUserService telegramUserService,
        ILogger<StartCommandModule> logger)
    {
        _telegramUserService = telegramUserService;
        _telegramBotClient = telegramBotClientFactory.GetClientAsync().Result;
        _logger = logger;
    }

    public async Task ExecuteAsync(Update update)
    {
        var messageText = update.Message?.Text;
        var chatInfo = update.Message?.Chat!;
        var chatId = chatInfo.Id;

        try
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var telegramAccount = await _telegramUserService.GetAccountByChatIdAsync(chatId);
                if (telegramAccount is null)
                {
                    var fullName = $"{update.Message!.From!.LastName ?? string.Empty} {update.Message.From!.FirstName ?? string.Empty}".Trim();
                    telegramAccount = await _telegramUserService.AddAccountDefaultAsync(fullName, chatId);
                }

                //Приветствуем
                await _telegramBotClient.SendMessage(chatInfo.Id, MessageParameters.Welcome, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
            }
        }
        catch (Exception exception)
        {
            var logMessage =
                $"Error in method {nameof(ExecuteAsync)} and module {TelegramCommand}. " +
                $"Message: {exception.Message}, stackTrace: {exception.StackTrace}";

            _logger.LogCritical(logMessage);

            await _telegramBotClient.SendMessage(chatId, MessageParameters.TechnicalErrorMessage, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
        }
    }

    #region Helpers

    

    #endregion
}
