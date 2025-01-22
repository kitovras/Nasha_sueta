using OurFuss.TelegramBot.EventGeneration;

namespace OurFuss.Web.Infrastructure.Extensions;

public static class InitConfigurationExtension
{
    public static async Task InitAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        await TelegramBotInit(scope);
    }

    #region Helpers

    private static async Task TelegramBotInit(IServiceScope scope)
    {
        var telegramBotService = scope.ServiceProvider.GetRequiredService<ITelegramClientFactory>();
        await telegramBotService.GetClientAsync();
    }

    #endregion
}
