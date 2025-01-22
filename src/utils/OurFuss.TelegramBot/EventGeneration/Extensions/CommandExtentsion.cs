using OurFuss.TelegramBot.EventGeneration.Enums;
using OurFuss.TelegramBot.EventGeneration.Parameters;

namespace OurFuss.TelegramBot.EventGeneration.Extensions;

public static class CommandExtentsion
{
    /// <summary>
    /// Get the command
    /// </summary>
    /// <param name="command">Command</param>
    /// <returns>Type of command</returns>
    public static TelegramCommandType GetComandType(this string command)
    {
        if (!command.StartsWith('/'))
            return TelegramCommandType.Unknown;

        if (command.ToLower().StartsWith(CommandParameters.Start))
            return TelegramCommandType.Start;

        if (command.ToLower().StartsWith(CommandParameters.Event))
            return TelegramCommandType.Event;

        return TelegramCommandType.Unknown;
    }

    /// <summary>
    /// Get the command type
    /// </summary>
    /// <param name="command">Command</param>
    /// <returns>String command</returns>
    public static string GetComandType(this TelegramCommandType command)
    {
        return command switch
        {
            TelegramCommandType.Start => CommandParameters.Start,
            TelegramCommandType.Event => CommandParameters.Event,
            _ => throw new ArgumentException("Command is unknown."),
        };
    }
}
