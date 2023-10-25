using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OurFuss.Core.Db.Entities.User;
using OurFuss.Utils.TelegramBot.Services;

namespace OurFuss.Api.Infrastructure.Extensions;

/// <summary>
/// Расширение для аккаунтов по умолчанию
/// </summary>
public static class InitConfigurationExtension
{
    public static async Task Init(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        await AccountsDefaultInit(scope);
        await TelegramBotInit(scope);
    }

    private static async Task TelegramBotInit(IServiceScope scope)
    {
        var telegramBotService = scope.ServiceProvider.GetRequiredService<ITelegramBotService>();
        telegramBotService.StartListener();
    }

    /// <summary>
    /// Инициализировать аккаунты по умолчанию
    /// </summary>
    /// <param name="serviceScope">Провайдер сервисов</param>
    /// <returns>Результат асинхронной задачи</returns>
    private static async Task AccountsDefaultInit(IServiceScope serviceScope)
    {
        var userViewer = new UserEntity
        {
            UserName = "userViewer",
        };

        var userAdmin = new UserEntity
        {
            UserName = "userAdmin",
        };

        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
        
        var userViewerExist = userManager.Users.FirstOrDefault(fod => fod.UserName == userViewer.UserName);
        if (userViewerExist is null)
        {
            var userViewerRole = new IdentityRole
            {
                Name = "viewer",
            };

            await userManager.CreateAsync(userViewer, "1gdgfdfddfFDKJJKBAFSSdgddff332fsdfdfs1*");
            await roleManager.CreateAsync(userViewerRole);
            await userManager.AddToRoleAsync(userViewer, userViewerRole.Name);
        }

        var userAdminExist = userManager.Users.FirstOrDefault(fod => fod.UserName == userAdmin.UserName);
        if (userAdminExist is null)
        {
            var userAdminRole = new IdentityRole
            {
                Name = "admin"
            };

            await userManager.CreateAsync(userAdmin, "3gdgfdfd*dfFDKFJADLAWAFSSdgddff332fsdfdfs2");
            await roleManager.CreateAsync(userAdminRole);
            await userManager.AddToRoleAsync(userViewer, userAdminRole.Name);
        }
    }
}
