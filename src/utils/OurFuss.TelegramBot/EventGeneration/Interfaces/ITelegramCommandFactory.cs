using OurFuss.TelegramBot.EventGeneration.Enums;

namespace OurFuss.TelegramBot.EventGeneration;

/// <summary>
/// Telegram command factory
/// </summary>
public interface ITelegramCommandFactory
{
    /// <summary>
    /// Get the module
    /// </summary>
    /// <param name="tgCommand">Type of command telegram</param>
    /// <returns></returns>
    ITelegramCommandModule? GetModule(TelegramCommandType tgCommand);
}
