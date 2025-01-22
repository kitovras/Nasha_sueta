using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OurFuss.Core.Sections.Events.Enums;
using OurFuss.Core.Sections.Events.Models.Domains;
using OurFuss.Core.Sections.Events.Services;
using OurFuss.Core.Sections.Telegram.Services;
using OurFuss.TelegramBot.EventGeneration;
using OurFuss.TelegramBot.EventGeneration.Parameters;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace OurFuss.Background.Hosted;

public class TelegramBackgroundService : IHostedService
{
    private readonly ILogger<TelegramBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    private Timer? _timerOnPublishEventToChannel;

    public TelegramBackgroundService(
        ILogger<TelegramBackgroundService> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timerOnPublishEventToChannel ??= new Timer(async (e) => await PublishEventToChannelAsync());

        StartTimerOnPublishEventToChannelAsync();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timerOnPublishEventToChannel?.Dispose();

        return Task.CompletedTask;
    }

    #region Logic

    private async Task PublishEventToChannelAsync()
    {
        _logger.LogInformation($"Start to work with timer: {nameof(PublishEventToChannelAsync)}.");

        using var scope = _serviceProvider.CreateScope();

        try
        {
            const int LOCAL_BATCH_SIZE = 10000;
            var eventStatus = EventStatusType.Pending;

            var _eventService = scope.ServiceProvider.GetRequiredService<IEventService>();
            var _telegramClientFactory = scope.ServiceProvider.GetRequiredService<ITelegramClientFactory>();
            var _telegramBotClient = _telegramClientFactory.GetClientAsync().Result;

            var count = await _eventService.GetEventCountAsync(eventStatus);            
            for (var index = 0; index < count; index += LOCAL_BATCH_SIZE)
            {
                var events = await _eventService.GetUserEventsAsync(index, LOCAL_BATCH_SIZE, eventStatus);
                foreach (var item in events)
                {
                    if (item.DeferredPublication > DateTime.UtcNow)
                        continue;

                    item.EventStatus = EventStatusType.Published;
                    await _eventService.SaveUserEventAsync(item);

                    PhotoSize? photoSize = null;
                    if (item.UserEventCustomImage is not null)
                    {
                        photoSize = new PhotoSize
                        {
                            FileId = item!.UserEventCustomImage!.FileId,
                            FileSize = item.UserEventCustomImage.FileSize,
                            Height = item.UserEventCustomImage.Height,
                            Width = item.UserEventCustomImage.Width,
                            FileUniqueId = item.UserEventCustomImage.FileUniqueId
                        };
                    }

                    _ = photoSize is not null
                        ? await _telegramBotClient.SendPhoto(SettingParameters.ChannelName, photoSize, item.MessageText, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html)
                        : await _telegramBotClient.SendMessage(SettingParameters.ChannelName, item.MessageText, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);

                    await _telegramBotClient.SendMessage(item.TelegramAccount!.ChatId, "Ваше событие опубликовано!", parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                }
            }

            _logger.LogInformation($"End to work with timer: {nameof(PublishEventToChannelAsync)}.");

            StartTimerOnPublishEventToChannelAsync();
        }
        catch (Exception exception)
        {
            _logger.LogCritical($"Error in method {nameof(PublishEventToChannelAsync)}. Message: {exception.Message}, stackTrace: {exception.StackTrace}");
        }
    }

    #endregion

    #region Setters

    private void StartTimerOnPublishEventToChannelAsync()
    {
        var time = TimeSpan.FromMinutes(SettingParameters.TimerForCheckingEventsPublicationInMinutes);
        _timerOnPublishEventToChannel?.Change(time, TimeSpan.Zero);
        _logger.LogInformation($"The task to run {nameof(StartTimerOnPublishEventToChannelAsync)} is started in {DateTime.UtcNow.AddMinutes(time.Minutes)}.");
    }

    #endregion
}
