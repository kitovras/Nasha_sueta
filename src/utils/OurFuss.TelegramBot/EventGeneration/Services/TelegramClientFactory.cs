using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OurFuss.TelegramBot.EventGeneration.Models.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OurFuss.TelegramBot.EventGeneration.Services;

public class TelegramClientFactory : ITelegramClientFactory
{
    private readonly TelegramBotOptions _telegramOptions;
    private readonly ILogger<TelegramClientFactory> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private static TelegramBotClient _telegramBotClient;

    public TelegramClientFactory(
       TelegramBotOptions telegramOptions,
       ILogger<TelegramClientFactory> logger,
       IServiceScopeFactory serviceScopeFactory)
    {
        _telegramOptions = telegramOptions;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<TelegramBotClient> GetClientAsync()
    {
        if (_telegramBotClient is not null)
            return _telegramBotClient;

        var telegramBotClientToken = _telegramOptions.Token;
        if (string.IsNullOrWhiteSpace(telegramBotClientToken))
            throw new ArgumentException("Telegram token is null or empty!");

        _telegramBotClient = new TelegramBotClient(telegramBotClientToken);

        if (_telegramOptions.IsPolling)
        {
            await InitPolling();
        }
        else
        {
            await InitWebhook();
        }

        return _telegramBotClient;
    }

    #region Helpers

    private async Task InitWebhook()
    {
        var webhookLink = _telegramOptions.Link;
        _logger.LogInformation("Webhook link set to: {0}", webhookLink);

        if (string.IsNullOrWhiteSpace(webhookLink))
            throw new ArgumentException("No webhook link from config (env or appsettings)");

        // Removes '/' if link ends with it
        if (webhookLink.EndsWith('/'))
            webhookLink = webhookLink.Remove(webhookLink.Length - 1);

        if (!string.IsNullOrWhiteSpace(_telegramOptions.IpAddress))
        {
            await _telegramBotClient.SetWebhook($"{webhookLink}/api/message/update", ipAddress: _telegramOptions.IpAddress);
            return;
        }

        await _telegramBotClient.SetWebhook($"{webhookLink}/api/message/update");
    }

    private async Task InitPolling()
    {
        await _telegramBotClient.DeleteWebhook();

        using var cancellationTokenSource = new CancellationTokenSource();
        var receiverOptions = new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() };

        _telegramBotClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cancellationTokenSource.Token
        );
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var telegramCommandService = scope.ServiceProvider.GetRequiredService<ITelegramListenerService>();
            await telegramCommandService.ProcessAsync(update);
        }
        catch (Exception exception)
        {
            _logger.LogCritical($"Error in method {nameof(HandleUpdateAsync)}. Message: {exception.Message}, stackTrace: {exception.StackTrace}");
        }
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogCritical($"Error in method {nameof(HandlePollingErrorAsync)}. Message: {exception.Message}, stackTrace: {exception.StackTrace}");

        return Task.CompletedTask;
    }

    #endregion
}