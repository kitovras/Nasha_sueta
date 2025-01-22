namespace OurFuss.TelegramBot.EventGeneration.Models.Options;

public class TelegramBotOptions
{
    /// <summary>
    /// Token for telegram bot from BotFather
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// It's link for webhook
    /// </summary>
    public string Link { get; set; } = string.Empty;

    /// <summary>
    /// A survey is being conducted
    /// </summary>
    public bool IsPolling { get; set; }

    public string? IpAddress { get; set; }
}
