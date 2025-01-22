using OurFuss.TelegramBot.EventGeneration.Enums;
using Telegram.Bot.Types;

namespace OurFuss.TelegramBot.EventGeneration;

/// <summary>
/// Telegram command module
/// </summary>
public interface ITelegramCommandModule
{
    /// <summary>
    /// Type of command telegram
    /// </summary>
    public abstract TelegramCommandType TelegramCommand { get; }

    /// <summary>
    /// Asynchronously execute
    /// </summary>
    /// <param name="update">This object represents an incoming update.</param>
    /// <returns></returns>
    public abstract Task ExecuteAsync(Update update);
}
