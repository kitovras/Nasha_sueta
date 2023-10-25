using Microsoft.Extensions.Options;
using OurFuss.Api.Infrastructure.Models;
using OurFuss.Utils.TelegramBot.Models;

namespace OurFuss.Api.Infrastructure.Extensions;

public static class OptionExtensions
{
    public static void AddOptions(WebApplicationBuilder builder)
    {
        builder.Services.Configure<TelegramBotOptions>(builder.Configuration.GetRequiredSection("TelegramBot"));
        builder.Services.AddScoped(ctg => ctg.GetService<IOptionsSnapshot<TelegramBotOptions>>()!.Value);
    }

    public static JwtOptions GetJwtOptions(this IConfiguration configuration)
    {
        return configuration.GetSection(JwtOptions.Section).Get<JwtOptions>()!;
    }
}