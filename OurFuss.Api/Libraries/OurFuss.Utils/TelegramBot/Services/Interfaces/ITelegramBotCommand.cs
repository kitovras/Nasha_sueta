using OurFuss.Utils.TelegramBot.Enums;
using OurFuss.Utils.TelegramBot.Models;

namespace OurFuss.Utils.TelegramBot.Services;

/// <summary>
/// Команда телеграм бота
/// </summary>
public interface ITelegramBotCommand
{
    /// <summary>
    /// Асинхронно получить ответ на команду
    /// </summary>
    /// <param name="telegramBotDetails">Детали телеграм бота</param>
    /// <returns>Ответ на команду</returns>
    Task<TelegramBotResponse> GetResponseToCommandAsync(TelegramBotDetails telegramBotDetails);

    /// <summary>
    /// Тип команды телеграм бота
    /// </summary>
    public TelegramBotCommandType TelegramBotCommandType { get; }
}
