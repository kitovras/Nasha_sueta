using OurFuss.Utils.TelegramBot.Enums;

namespace OurFuss.Utils.TelegramBot.Services;

/// <summary>
/// Фабрика команд телеграм бота
/// </summary>
public interface ITelegramBotCommandFactory
{
    /// <summary>
    /// Получить команду телеграм бота
    /// </summary>
    /// <param name="telegramBotCommandType">Тип команды телеграм бота</param>
    /// <returns>Команда телеграм бота</returns>
    ITelegramBotCommand GetTelegramBotCommand(TelegramBotCommandType telegramBotCommandType);
}
