using OurFuss.Core.Db.Entities.User;
using OurFuss.Core.Modules.Common.Repositories;
using OurFuss.Utils.TelegramBot.Enums;
using OurFuss.Utils.TelegramBot.Extensions;
using OurFuss.Utils.TelegramBot.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace OurFuss.Utils.TelegramBot.Services.Modules;

/// <inheritdoc/>
public class TelegramBotProfileModule : ITelegramBotCommand
{
    /// <summary>
    /// Репозиторий сущностей
    /// </summary>
    private readonly IEntityRepository _entityRepository;
    
    /// <inheritdoc/>
    public TelegramBotCommandType TelegramBotCommandType => TelegramBotCommandType.Profile;

    public TelegramBotProfileModule(IEntityRepository entityRepository)
    {
        _entityRepository = entityRepository;
    }

    /// <inheritdoc/>
    public async Task<TelegramBotResponse> GetResponseToCommandAsync(TelegramBotDetails telegramBotDetails)
    {
        if (telegramBotDetails.ProfileSaveCommand)
        {
            if (int.TryParse(telegramBotDetails.RegionCode, out int regionCode))
            {
                await SaveRegionCodeAsync(regionCode, telegramBotDetails.ChatId);
                return GetResponseSuccess();
            }
        }

        return GetResponseDefault();
    }

    #region Helpers

    private static TelegramBotResponse GetResponseSuccess()
    {
        var message = "Код региона успешно сохранен!";

        var buttons = new List<InlineKeyboardButton>()
        {
            new InlineKeyboardButton("На главную")
            {
                CallbackData = TelegramBotCommandType.Start.GetComandType(),
            }
        };

        var telegramBotResponse = new TelegramBotResponse()
        {
            Message = message,
            Buttons = new InlineKeyboardMarkup(buttons)
        };

        return telegramBotResponse;
    }

    private async Task SaveRegionCodeAsync(int regionCode, long chatId)
    {
        var telegramAccount = _entityRepository.GetQueryable<TelegramAccountEntity>().Single(fod => fod.ChatId == chatId);
        telegramAccount.RegionCodeSelected = regionCode;

        await _entityRepository.UpdateAsync(telegramAccount);
    }

    private static TelegramBotResponse GetResponseDefault()
    {
        var message =
            "Введи циферный код регион без кавычек пробелов и прочих символов в следующем формате: \n" +
            "/profile - <b>'КОД_РЕГИОНА'</b>";

        var buttons = new List<InlineKeyboardButton>()
        {
            new InlineKeyboardButton("На главную")
            {
                CallbackData = TelegramBotCommandType.Start.GetComandType(),
            }
        };

        var telegramBotResponse = new TelegramBotResponse()
        {
            Message = message,
            Buttons = new InlineKeyboardMarkup(buttons)
        };

        return telegramBotResponse;
    }

    #endregion
}
