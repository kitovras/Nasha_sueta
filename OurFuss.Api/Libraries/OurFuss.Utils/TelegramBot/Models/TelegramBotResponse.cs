using Telegram.Bot.Types.ReplyMarkups;

namespace OurFuss.Utils.TelegramBot.Models;

/// <summary>
/// Ответ телеграм бота
/// </summary>
public class TelegramBotResponse
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Кнопки
    /// </summary>
    public IReplyMarkup? Buttons { get; set; }
}
