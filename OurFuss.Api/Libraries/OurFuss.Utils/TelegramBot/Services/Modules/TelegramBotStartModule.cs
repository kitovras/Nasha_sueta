using OurFuss.Core.Db.Entities.User;
using OurFuss.Core.Modules.Common.Repositories;
using OurFuss.Utils.TelegramBot.Enums;
using OurFuss.Utils.TelegramBot.Extensions;
using OurFuss.Utils.TelegramBot.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace OurFuss.Utils.TelegramBot.Services.Modules;

/// <inheritdoc/>
public class TelegramBotStartModule : ITelegramBotCommand
{
    /// <summary>
    /// Репозиторий сущностей
    /// </summary>
    private readonly IEntityRepository _entityRepository;

    /// <inheritdoc/>
    public TelegramBotCommandType TelegramBotCommandType => TelegramBotCommandType.Start;

    public TelegramBotStartModule(IEntityRepository entityRepository)
    {
        _entityRepository = entityRepository;
    }

    /// <inheritdoc/>
    public async Task<TelegramBotResponse> GetResponseToCommandAsync(TelegramBotDetails telegramBotDetails)
    {
        var isTelegramAccountNew = false;
        var telegramAccount = _entityRepository.GetQueryable<TelegramAccountEntity>().FirstOrDefault(fod => fod.ChatId == telegramBotDetails.ChatId);

        if (telegramAccount is null)
        {
            isTelegramAccountNew = true;
            telegramAccount = await SaveAccountAsync(telegramBotDetails.Message);
        }

        var response = new TelegramBotResponse
        {
            Message = GetMessage(telegramAccount, isTelegramAccountNew),
            Buttons = GetNavigationButtons(),
        };
        
        return await Task.FromResult(response);
    }

    #region Helpers

    private async Task<TelegramAccountEntity> SaveAccountAsync(Message message)
    {
        var fullName = $"{message.From!.LastName ?? string.Empty} {message.From!.FirstName ?? string.Empty}".Trim();

        var telegramAccount = new TelegramAccountEntity
        {
            ChatId = message.Chat.Id,
            FullName = fullName,
            DateTimeAdded = DateTime.UtcNow,
            DateTimeModified = DateTime.UtcNow,
            RegionCodeSelected = 0,
        };

        await _entityRepository.AddAsync(telegramAccount);
        return telegramAccount;
    }

    private static IReplyMarkup GetNavigationButtons()
    {
        //https://qaa-engineer.ru/c-sozdanie-knopok-v-bote-telegram/


        var buttons = new List<InlineKeyboardButton>()
        {
            new InlineKeyboardButton("Мой регион")
            {
                CallbackData = TelegramBotCommandType.Profile.GetComandType(),
            },
            new InlineKeyboardButton("События")
            {
                CallbackData = TelegramBotCommandType.Events.GetComandType()
            }
        };

        return new InlineKeyboardMarkup(buttons);
    }

    private static string GetMessage(TelegramAccountEntity telegramAccount, bool isTelegramAccountNew)
    {
        var volatileText = isTelegramAccountNew || telegramAccount.RegionCodeSelected == 0
            ? "Но, сейчас у тебя не выбран регион. Чтобы указать его, жми на кнопку <b>Мой регион</b>, чтобы я понимал, где ты находишься."
            : $"Это твой текущий регион: <b>{telegramAccount.RegionCodeSelected}</b>. Если ты в другом месте, то измени его, чтобы получать события относительно своего текущего расположения.";

        return 
            $"Приветствуем <b>{telegramAccount.FullName}</b>! \n" +
            $"Хочешь узнать обо всех мероприятиях и события в твоём городе? Тогда, ты обратился по адресу! " +
            volatileText;
    }

    #endregion
}
