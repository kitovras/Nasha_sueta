using Telegram.Bot;

namespace OurFuss.TelegramBot.EventGeneration;

public interface ITelegramClientFactory
{
    Task<TelegramBotClient> GetClientAsync();
}
