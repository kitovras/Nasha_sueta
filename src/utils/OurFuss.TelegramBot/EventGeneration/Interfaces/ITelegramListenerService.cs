using Telegram.Bot.Types;

namespace OurFuss.TelegramBot.EventGeneration;

/// <summary>
/// Telegram listening service
/// </summary>
public interface ITelegramListenerService
{
    /// <summary>
    /// Asynchronous process
    /// </summary>
    /// <param name="update">This object represents an incoming update.</param>
    /// <returns>Asynchronous task</returns>
    Task ProcessAsync(Update update);
}
