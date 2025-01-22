using Microsoft.Extensions.Logging;
using OurFuss.Core.Sections.Telegram.Services;
using OurFuss.TelegramBot.EventGeneration.Enums;
using OurFuss.TelegramBot.EventGeneration.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OurFuss.TelegramBot.EventGeneration.Services;

/// <inheritdoc/>
public class TelegramListenerService : ITelegramListenerService
{
    private readonly ILogger<TelegramListenerService> _logger;
    private readonly TelegramBotClient _telegramBotClient;
    private readonly ITelegramCommandFactory _telegramCommandFactory;
    private readonly ITelegramUserService _telegramUserService;

    public TelegramListenerService(
        ILogger<TelegramListenerService> logger,
        ITelegramCommandFactory telegramCommandFactory,
        ITelegramUserService telegramUserService,
        ITelegramClientFactory telegramClientFactory)
    {
        _logger = logger;
        _telegramBotClient = telegramClientFactory.GetClientAsync().Result;
        _telegramCommandFactory = telegramCommandFactory;
        _telegramUserService = telegramUserService;
    }

    /// <inheritdoc/>
    public async Task ProcessAsync(Update update)
    {
        if (update == null)
            throw new ArgumentException("Update from telegram is null!");

        if (update.Message?.Chat == null && update.CallbackQuery == null && update.MyChatMember == null)
            throw new ArgumentNullException("Update type is not valid for this service (need types: 'Message' || 'CallbackQuery' || 'MyChatMember')");

        try
        {
            _logger.LogInformation("Start processing telegram update.");

            switch (update.Type)
            {
                case UpdateType.Message:
                    {
                        await MessageProcessingAsync(update);
                    }
                    break;
                case UpdateType.CallbackQuery:
                    {
                        await CallbackQueryProcessingAsync(update);
                    }
                    break;
                case UpdateType.MyChatMember:
                    {
                        await MyChatMemberProcessingAsync(update);
                    }
                    break;
                default: throw new ArgumentException("Update type is unknown!");
            }

            _logger.LogInformation("End processing telegram update.");
        }
        catch (Exception exception)
        {
            _logger.LogCritical($"Error in method {nameof(ProcessAsync)}. Message: {exception.Message}, stackTrace: {exception.StackTrace}");
        }
    }

    #region Helpers

    private async Task MessageProcessingAsync(Update update)
    {
        var messageText = string.IsNullOrWhiteSpace(update.Message!.Text)
            ? update.Message!.Caption ?? string.Empty
            : update.Message!.Text ?? string.Empty;

        var tgCommand = messageText.GetComandType();
        var module = _telegramCommandFactory.GetModule(tgCommand);

        //Если пользователь существует и команда не определена - значит пишут текст. Воспринимаем этот как попытку добавления события
        if (_telegramUserService.UserExistAsync(update.Message.Chat.Id).Result && 
            tgCommand is TelegramCommandType.Unknown &&
            !string.IsNullOrWhiteSpace(messageText))
        {
            module = _telegramCommandFactory.GetModule(TelegramCommandType.Event);
        }

        await module!.ExecuteAsync(update);
    }

    private async Task CallbackQueryProcessingAsync(Update update)
    {
        var callbackQueryString = update.CallbackQuery!.Data ?? string.Empty;
        var tgCommand = callbackQueryString.GetComandType();
        var module = _telegramCommandFactory.GetModule(tgCommand);

        await module!.ExecuteAsync(update);
    }

    private async Task MyChatMemberProcessingAsync(Update update)
    {
        var module = _telegramCommandFactory.GetModule(TelegramCommandType.Membership);
        await module!.ExecuteAsync(update);
    }

    #endregion
}
