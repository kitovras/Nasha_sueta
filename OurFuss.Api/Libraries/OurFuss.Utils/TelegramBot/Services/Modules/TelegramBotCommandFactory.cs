using OurFuss.Utils.TelegramBot.Enums;

namespace OurFuss.Utils.TelegramBot.Services.Modules;

/// <inheritdoc/>
public class TelegramBotCommandFactory : ITelegramBotCommandFactory
{
    /// <summary>
    /// Команда телеграм бота
    /// </summary>
    private readonly IEnumerable<ITelegramBotCommand> _telegramBotCommand;

    public TelegramBotCommandFactory(IEnumerable<ITelegramBotCommand> telegramBotCommand)
    {
        _telegramBotCommand = telegramBotCommand;
    }

    /// <inheritdoc/>
    public ITelegramBotCommand GetTelegramBotCommand(TelegramBotCommandType telegramBotCommandType)
    {
        return _telegramBotCommand.Single(fod => fod.TelegramBotCommandType == telegramBotCommandType);
    }
}
