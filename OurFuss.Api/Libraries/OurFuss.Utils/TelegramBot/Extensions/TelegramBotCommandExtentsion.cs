using OurFuss.Utils.TelegramBot.Enums;

namespace OurFuss.Utils.TelegramBot.Extensions;

/// <summary>
/// Расширение команд для телеграм бота
/// </summary>
internal static class TelegramBotCommandExtentsion
{
    /// <summary>
    /// Получить команду
    /// </summary>
    /// <param name="command">Команда</param>
    /// <returns>Тип команды</returns>
    public static TelegramBotCommandType GetComandType(this string? command)
    {
        return command switch
        {
            "/start" => TelegramBotCommandType.Start,
            "/events" => TelegramBotCommandType.Events,
            "/profile" => TelegramBotCommandType.Profile,
            _ => TelegramBotCommandType.Unknown
        };
    }

    /// <summary>
    /// Получить команду
    /// </summary>
    /// <param name="command">Команда</param>
    /// <returns>Строковая команда</returns>
    public static string GetComandType(this TelegramBotCommandType command)
    {
        return command switch
        {
            TelegramBotCommandType.Start => "/start",
            TelegramBotCommandType.Events => "/events",
            TelegramBotCommandType.Profile => "/profile",
            TelegramBotCommandType.Unknown => string.Empty,
        };
    }
}
