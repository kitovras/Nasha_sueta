using Microsoft.Extensions.Options;
using OurFuss.TelegramBot.EventGeneration.Models.Options;

namespace OurFuss.Web.Infrastructure.Extensions;

public static class OptionExtensions
{
    public static void AddOptions(WebApplicationBuilder builder)
    {
        builder.Services.Configure<TelegramBotOptions>(builder.Configuration.GetSection("TelegramBot"));
        builder.Services.AddScoped(ctg => ctg.GetService<IOptionsSnapshot<TelegramBotOptions>>()!.Value);
    }
}
