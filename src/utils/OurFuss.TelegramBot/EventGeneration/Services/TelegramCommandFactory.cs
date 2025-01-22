using OurFuss.TelegramBot.EventGeneration.Enums;

namespace OurFuss.TelegramBot.EventGeneration.Services;

/// <inheritdoc/>
public class TelegramCommandFactory : ITelegramCommandFactory
{
    /// <summary>
    /// The telegram command module
    /// </summary>
    private readonly IEnumerable<ITelegramCommandModule> _telegramBotCommand;

    public TelegramCommandFactory(IEnumerable<ITelegramCommandModule> telegramBotCommand)
    {
        _telegramBotCommand = telegramBotCommand;
    }

    /// <inheritdoc/>
    public ITelegramCommandModule? GetModule(TelegramCommandType telegramCommandType)
    {
        return _telegramBotCommand.SingleOrDefault(fod => fod.TelegramCommand == telegramCommandType);
    }
}
