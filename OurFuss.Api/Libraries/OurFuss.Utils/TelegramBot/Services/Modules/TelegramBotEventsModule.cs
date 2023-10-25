using OurFuss.Utils.TelegramBot.Enums;
using OurFuss.Utils.TelegramBot.Models;

namespace OurFuss.Utils.TelegramBot.Services.Modules;

public class TelegramBotEventsModule : ITelegramBotCommand
{
    public TelegramBotCommandType TelegramBotCommandType => TelegramBotCommandType.Events;

    public TelegramBotEventsModule()
    {

    }

    public Task<TelegramBotResponse> GetResponseToCommandAsync(TelegramBotDetails telegramBotDetails)
    {
        throw new NotImplementedException();
    }
}
