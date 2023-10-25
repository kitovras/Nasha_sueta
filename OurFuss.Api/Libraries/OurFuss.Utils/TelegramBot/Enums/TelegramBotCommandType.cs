namespace OurFuss.Utils.TelegramBot.Enums;

/// <summary>
/// Типы пользовательских команд
/// </summary>
public enum TelegramBotCommandType
{
    /// <summary>
    /// Не определена
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Начало
    /// </summary>
    Start = 1,

    /// <summary>
    /// События
    /// </summary>
    Events = 2,

    /// <summary>
    /// Профиль
    /// </summary>
    Profile = 3,
}
