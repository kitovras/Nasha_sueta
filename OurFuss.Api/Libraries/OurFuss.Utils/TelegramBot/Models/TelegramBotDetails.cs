using Telegram.Bot.Types;

namespace OurFuss.Utils.TelegramBot.Models;

/// <summary>
/// Детали телеграм бота
/// </summary>
public class TelegramBotDetails
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public Message Message { get; set; } = new();

    /// <summary>
    /// Ответный запрос
    /// </summary>
    public CallbackQuery CallbackQuery { get; set; } = new();

    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public long ChatId { get; set; }

    /// <summary>
    /// Код региона
    /// </summary>
    public string? RegionCode { get; set; } = string.Empty;

    /// <summary>
    /// Команда сохранения профиля
    /// </summary>
    public bool ProfileSaveCommand { get; set; } = false;
}
