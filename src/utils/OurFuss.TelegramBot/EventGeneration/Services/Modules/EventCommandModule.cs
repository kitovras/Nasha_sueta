using Microsoft.Extensions.Logging;
using OurFuss.Core.Sections.Events.Enums;
using OurFuss.Core.Sections.Events.Models.Domains;
using OurFuss.Core.Sections.Events.Services;
using OurFuss.Core.Sections.Telegram.Enums;
using OurFuss.Core.Sections.Telegram.Services;
using OurFuss.TelegramBot.EventGeneration.Enums;
using OurFuss.TelegramBot.EventGeneration.Extensions;
using OurFuss.TelegramBot.EventGeneration.Parameters;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OurFuss.TelegramBot.EventGeneration.Services.Modules;

/// <summary>
/// Event management module
/// </summary>
public class EventCommandModule : ITelegramCommandModule
{
    /// <inheritdoc/>
    public TelegramCommandType TelegramCommand => TelegramCommandType.Event;

    /// <summary>
    /// Logger
    /// </summary>
    private readonly ILogger<EventCommandModule> _logger;

    /// <summary>
    /// A client to use the Telegram Bot API
    /// </summary>
    private readonly TelegramBotClient _telegramBotClient;

    private readonly ITelegramUserService _telegramUserService;

    private readonly IEventService _eventService;

    public EventCommandModule(
        ITelegramClientFactory telegramBotClientFactory,
        ILogger<EventCommandModule> logger,
        ITelegramUserService telegramUserService,
        IEventService eventService)
    {
        _telegramUserService = telegramUserService;
        _telegramBotClient = telegramBotClientFactory.GetClientAsync().Result;
        _eventService = eventService;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task ExecuteAsync(Update update)
    {
        long chatId = 0;

        try
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var messageText = string.IsNullOrWhiteSpace(update.Message!.Text)
                    ? update.Message!.Caption ?? string.Empty
                    : update.Message!.Text ?? string.Empty;

                PhotoSize? photo = update.Message.Photo?.LastOrDefault();

                var chatInfo = update.Message?.Chat!;
                chatId = chatInfo.Id;

                if (string.IsNullOrWhiteSpace(messageText))
                {
                    await _telegramBotClient.SendMessage(chatId, "Пустое собщение может привести к блокировке!", parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                    return;
                }

                await MessageProcessingAsync(chatId, messageText!, photo);
            }
            else if(update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                var callbackData = update!.CallbackQuery!.Data;
                var chatInfo = update.CallbackQuery.Message!.Chat;
                var messageId = update.CallbackQuery.Message.MessageId;
                chatId = chatInfo.Id;

                await CallbackQueryProcessingAsync(chatId, callbackData ?? string.Empty);
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

    private async Task MessageProcessingAsync(long chatId, string messageText, PhotoSize? photo)
    {
        var (subcommandType, customEventId, messageString, deferredPublicationDay) = GetQueries(messageText);
        if (subcommandType == CustomEventSubcommandType.Unknown && customEventId == Guid.Empty)
        {
            var admins = await _telegramUserService.GetAdminsAsync();
            if (!admins.Any())
                throw new NullReferenceException($"The administrator was not found!");

            var user = await _telegramUserService.GetAccountByChatIdAsync(chatId);

            var addUserEventModelInput = new UserEventCustom
            {
                MessageText = messageText,
                TelegramId = user!.Id,
            };

            if (photo is not null)
            {
                addUserEventModelInput.UserEventCustomImage = new UserEventCustomImage
                {
                    FileId = photo.FileId,
                    FileSize = photo.FileSize,
                    Height = photo.Height,
                    Width = photo.Width,
                    FileUniqueId = photo.FileUniqueId
                };
            }

            await _eventService.AddUserEventAsync(addUserEventModelInput);

            var buttons = GetButtonsWithEvent(addUserEventModelInput.Id);
            var message =
                "Команды для копирования:\n" +
                $"<code>/event&{CustomEventSubcommandType.Reject}&{addUserEventModelInput.Id}</code>";

            _ = photo is not null
                ? await _telegramBotClient.SendPhoto(admins.FirstOrDefault()!.ChatId, photo, addUserEventModelInput.MessageText, replyMarkup: buttons, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html)
                : await _telegramBotClient.SendMessage(admins.FirstOrDefault()!.ChatId, addUserEventModelInput.MessageText, replyMarkup: buttons, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);

            await _telegramBotClient.SendMessage(admins.FirstOrDefault()!.ChatId, message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
        }
        else
        {
            var userEventCustom = await _eventService.GetUserEventCustomWithTelegramAccountAsync(customEventId);
            if (userEventCustom is null)
                return;

            //Уведомляем владельца и удаляем
            await _telegramBotClient.SendMessage(userEventCustom.TelegramAccount!.ChatId, messageString, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);            
            await _eventService.UserEventCustomRemoveByIdAsync(customEventId);
        }
    }

    private async Task CallbackQueryProcessingAsync(long chatId, string callbackData)
    {
        var (subcommandType, customEventId, messageString, deferredPublicationDay) = GetQueries(callbackData);
        if (subcommandType == CustomEventSubcommandType.DefPublication && deferredPublicationDay == 0)
        {
            var admins = await _telegramUserService.GetAdminsAsync();
            if (!admins.Any())
                throw new NullReferenceException($"The administrator was not found!");

            var buttons = GetButtonsWithDeferredPublicationEvent(customEventId);
            await _telegramBotClient.SendMessage(admins.FirstOrDefault()!.ChatId, MessageParameters.DeferredPublicationMessage, replyMarkup: buttons, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
        }
        //Отложенная публикация
        else if (subcommandType == CustomEventSubcommandType.DefPublication && deferredPublicationDay != 0)
        {
            var userEventCustom = await _eventService.GetUserEventAsync(customEventId);
            if (userEventCustom is null)
                return;

            userEventCustom.EventStatus = EventStatusType.Pending;
            userEventCustom.DeferredPublication = DateTime.UtcNow.AddDays(deferredPublicationDay);
            await _eventService.SaveUserEventAsync(userEventCustom);

            var admins = await _telegramUserService.GetAdminsAsync();
            if (!admins.Any())
                throw new NullReferenceException($"The administrator was not found!");

            var message = $"Успешно сохранено. Событие опубликуются в <b>{userEventCustom.DeferredPublication.Value}</b>";
            await _telegramBotClient.SendMessage(admins.FirstOrDefault()!.ChatId, message, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
        }
        //Мгновенная публикация
        else
        {
            var userEventCustom = await _eventService.GetUserEventFullAsync(customEventId);
            if (userEventCustom is null)
                return;

            PhotoSize? photoSize = null;
            if (userEventCustom.UserEventCustomImage is not null)
            {
                photoSize = new PhotoSize
                {
                    FileId = userEventCustom!.UserEventCustomImage!.FileId,
                    FileSize = userEventCustom.UserEventCustomImage.FileSize,
                    Height = userEventCustom.UserEventCustomImage.Height,
                    Width = userEventCustom.UserEventCustomImage.Width,
                    FileUniqueId = userEventCustom.UserEventCustomImage.FileUniqueId
                };
            }

            userEventCustom.EventStatus = EventStatusType.Published;
            await _eventService.SaveUserEventAsync(userEventCustom);

            _ = photoSize is not null
                ? await _telegramBotClient.SendPhoto(SettingParameters.ChannelName, photoSize, userEventCustom.MessageText, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html)
                : await _telegramBotClient.SendMessage(SettingParameters.ChannelName, userEventCustom.MessageText, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);

            await _telegramBotClient.SendMessage(userEventCustom.TelegramAccount!.ChatId, "Ваше событие опубликовано!", parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
        }       
    }

    private static InlineKeyboardMarkup GetButtonsWithDeferredPublicationEvent(Guid eventId)
    {
        var inlineKeyboardButtonList = new List<InlineKeyboardButton[]>();

        var firstRow = new InlineKeyboardButton[]
        {
            InlineKeyboardButton.WithCallbackData(text: "День", $"{TelegramCommandType.Event.GetComandType()}&{CustomEventSubcommandType.DefPublication}&{eventId}&1"),
            InlineKeyboardButton.WithCallbackData(text: "Два", $"{TelegramCommandType.Event.GetComandType()}&{CustomEventSubcommandType.DefPublication}&{eventId}&2"),
            InlineKeyboardButton.WithCallbackData(text: "Три", $"{TelegramCommandType.Event.GetComandType()}&{CustomEventSubcommandType.DefPublication}&{eventId}&3"),
            InlineKeyboardButton.WithCallbackData(text: "Четыре", $"{TelegramCommandType.Event.GetComandType()}&{CustomEventSubcommandType.DefPublication}&{eventId}&4"),
            InlineKeyboardButton.WithCallbackData(text: "Пять", $"{TelegramCommandType.Event.GetComandType()}&{CustomEventSubcommandType.DefPublication}&{eventId}&5"),
            InlineKeyboardButton.WithCallbackData(text: "Шесть", $"{TelegramCommandType.Event.GetComandType()}&{CustomEventSubcommandType.DefPublication}&{eventId}&6"),
            InlineKeyboardButton.WithCallbackData(text: "Семь", $"{TelegramCommandType.Event.GetComandType()}&{CustomEventSubcommandType.DefPublication}&{eventId}&7"),
        };

        var inlineKeyboardResult = new InlineKeyboardMarkup(inlineKeyboardButtonList);
        inlineKeyboardButtonList.Add(firstRow);

        return inlineKeyboardResult;
    }

    private static InlineKeyboardMarkup GetButtonsWithEvent(Guid eventId)
    {
        var inlineKeyboardButtonList = new List<InlineKeyboardButton[]>();

        var firstRow = new InlineKeyboardButton[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Опубликовать сейчас", $"{TelegramCommandType.Event.GetComandType()}&{CustomEventSubcommandType.Accepted}&{eventId}"),            
        };

        var lastRow = new InlineKeyboardButton[]
        {
            InlineKeyboardButton.WithCallbackData(text: "Опубликовать позже", $"{TelegramCommandType.Event.GetComandType()}&{CustomEventSubcommandType.DefPublication}&{eventId}"),
        };

        var inlineKeyboardResult = new InlineKeyboardMarkup(inlineKeyboardButtonList);
        inlineKeyboardButtonList.Add(firstRow);
        inlineKeyboardButtonList.Add(lastRow);

        return inlineKeyboardResult;
    }

    private static (CustomEventSubcommandType subcommandType, Guid customEventId, string messageString, int deferredPublicationDay)
        GetQueries(string text)
    {
        var telegramCommandType = TelegramCommandType.Unknown;
        var subcommandType = CustomEventSubcommandType.Unknown;
        var customEventId = Guid.Empty;
        var messageString = string.Empty;
        var deferredPublicationDay = 0;

        var queriesTwo = text.Split(" ").Select(s => s.Trim()).Distinct().ToList();

        if (queriesTwo.Count >= 1)
        {
            var queriesOne = queriesTwo.First().Split("&").ToList();
            foreach (var query in queriesOne)
            {
                if (query.StartsWith("/"))
                {
                    telegramCommandType = query.GetComandType();
                    continue;
                }

                if (subcommandType == CustomEventSubcommandType.Unknown)
                {
                    subcommandType = GetCustomEventSubcommandType(query);
                    continue;
                }

                if (customEventId == Guid.Empty)
                {
                    customEventId = customEventId == Guid.Empty ? new Guid(query) : customEventId;
                    continue;
                }

                if (int.TryParse(query, out int result))
                {
                    deferredPublicationDay = result;
                    continue;
                }
            }
        }

        var commandText = queriesTwo.First();
        queriesTwo.Remove(commandText);
        messageString = string.Join("", queriesTwo);

        return (subcommandType, customEventId, messageString, deferredPublicationDay);
    }

    private static CustomEventSubcommandType GetCustomEventSubcommandType(string parameter)
    {
        return parameter switch
        {
            "Accepted" => CustomEventSubcommandType.Accepted,
            "Reject" => CustomEventSubcommandType.Reject,
            "DefPublication" => CustomEventSubcommandType.DefPublication,
            _ => CustomEventSubcommandType.Unknown,
        };
    }

    #endregion
}
