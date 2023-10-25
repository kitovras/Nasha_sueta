using Microsoft.Extensions.DependencyInjection;
using OurFuss.Utils.TelegramBot.Enums;
using OurFuss.Utils.TelegramBot.Extensions;
using OurFuss.Utils.TelegramBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OurFuss.Utils.TelegramBot.Services;

/// <inheritdoc/>
public class TelegramBotService : ITelegramBotService
{
    /// <summary>
    /// Телеграм бот api-клиент
    /// </summary>
    private TelegramBotClient _botClient;

    /// <summary>
    /// Фабрика области видимости
    /// </summary>
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public TelegramBotService(
        TelegramBotOptions telegramBotOptions,
        IServiceScopeFactory serviceScopeFactory)
    {
        _botClient = new TelegramBotClient(telegramBotOptions.Token);
        _serviceScopeFactory = serviceScopeFactory;
    }

    /// <inheritdoc/>
    public void StartListener()
    {
        _botClient.StartReceiving(CommandHandlerAsync, ErrorHandlerAsync);
    }

    #region Handlers

    /// <summary>
    /// Асинхронный обработчик ошибок
    /// </summary>
    /// <param name="client">Клиент телеграм бота</param>
    /// <param name="exception">Исключение</param>
    /// <param name="token">Токен отмены задачи</param>
    /// <returns>Асинхронная задача</returns>
    private Task ErrorHandlerAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Асинхронный обработчик команд
    /// </summary>
    /// <param name="client">Клиент телеграм бота</param>
    /// <param name="update">Объект представляющий входящее обновление</param>
    /// <param name="token">Токен отмены задачи</param>
    /// <returns>Асинхронная задача</returns>
    private async Task CommandHandlerAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message is not null)
        {
            await MessageProcessingAsync(update);
        }
        else if (update.CallbackQuery is not null)
        {
            await CallbackQueryProcessingAsync(update);
        }
    }

    #endregion

    #region Helpers

    private async Task CallbackQueryProcessingAsync(Update update)
    {
        var chatId = update.CallbackQuery!.Message!.Chat.Id;
        var commandType = update.CallbackQuery.Data.GetComandType();

        if (commandType == TelegramBotCommandType.Unknown)
        {
            var message = "Воспользуетесь перечнем команд, для взаимодействия с ботом.";
            await _botClient.SendTextMessageAsync(chatId, message);
            return;
        }

        var telegramBotDetails = new TelegramBotDetails
        {
            CallbackQuery = update.CallbackQuery,
            ChatId = chatId,
        };

        //Для вызова dbContext из фонового процесса
        using var scope = _serviceScopeFactory.CreateScope();
        var _telegramBotCommandFactory = scope.ServiceProvider.GetService<ITelegramBotCommandFactory>();

        var module = _telegramBotCommandFactory!.GetTelegramBotCommand(commandType);
        var response = await module.GetResponseToCommandAsync(telegramBotDetails);

        await _botClient.SendTextMessageAsync(chatId: chatId, text: response.Message, replyMarkup: response.Buttons, parseMode: ParseMode.Html);
    }

    private async Task MessageProcessingAsync(Update update)
    {
        var chatId = update.Message!.Chat.Id;
        switch (update.Message.Type)
        {
            case MessageType.Text:
                {
                    //Для вызова dbContext из фонового процесса
                    using var scope = _serviceScopeFactory.CreateScope();
                    var _telegramBotCommandFactory = scope.ServiceProvider.GetService<ITelegramBotCommandFactory>();

                    var commandDetails = GetComandTypeWithTelegramBotDetails(update);
                    if (commandDetails.Key == TelegramBotCommandType.Unknown)
                        goto default;

                    var module = _telegramBotCommandFactory!.GetTelegramBotCommand(commandDetails.Key);
                    var response = await module.GetResponseToCommandAsync(commandDetails.Value);

                    //https://qaa-engineer.ru/c-sozdanie-knopok-v-bote-telegram/
                    await _botClient.SendTextMessageAsync(chatId: chatId, text: response.Message, replyMarkup: response.Buttons, parseMode: ParseMode.Html);
                }
                break;
            default:
                {
                    var message = "Воспользуетесь перечнем команд, для взаимодействия с ботом.";
                    await _botClient.SendTextMessageAsync(chatId, message);
                }
                break;
        }
    }

    private static KeyValuePair<TelegramBotCommandType, TelegramBotDetails> GetComandTypeWithTelegramBotDetails(Update update)
    {
        var textArray = update.Message!.Text?.Split("-");
        var command = textArray?.FirstOrDefault()?.Trim();
        var commandType = command.GetComandType();

        var telegramBotDetails = new TelegramBotDetails
        {
            Message = update.Message,
            ChatId = update.Message!.Chat.Id,
        };

        if (commandType == TelegramBotCommandType.Profile)
        {
            telegramBotDetails.RegionCode = textArray?.LastOrDefault()?.Trim();
            telegramBotDetails.ProfileSaveCommand = true;
        }    

        return new KeyValuePair<TelegramBotCommandType, TelegramBotDetails>(commandType, telegramBotDetails);
    }

    #endregion
}