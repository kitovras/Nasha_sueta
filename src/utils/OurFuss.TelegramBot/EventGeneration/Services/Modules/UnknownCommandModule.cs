using Microsoft.Extensions.Logging;
using OurFuss.TelegramBot.EventGeneration.Enums;
using OurFuss.TelegramBot.EventGeneration.Parameters;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OurFuss.TelegramBot.EventGeneration.Services.Modules;

/// <summary>
/// Unknown command module
/// </summary>
public class UnknownCommandModule : ITelegramCommandModule
{
    private readonly ILogger<TelegramListenerService> _logger;
    private readonly TelegramBotClient _telegramBotClient;

    public TelegramCommandType TelegramCommand => TelegramCommandType.Unknown;

    public UnknownCommandModule(
        ILogger<TelegramListenerService> logger,
        ITelegramClientFactory telegramBotClientFactory)
    {
        _logger = logger;
        _telegramBotClient = telegramBotClientFactory.GetClientAsync().Result;
    }

    public async Task ExecuteAsync(Update update)
    {
        var messageText = update.Message?.Text;
        var chatInfo = update.Message?.Chat;

        await _telegramBotClient.SendMessage(chatInfo.Id, MessageParameters.UnknownCommandMessage, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
    }
}
