using OurFuss.Api.Facades;
using OurFuss.Core.Modules.Activities.Repositories;
using OurFuss.Core.Modules.Activities.Services;
using OurFuss.Core.Modules.Common.Repositories;
using OurFuss.Utils.TelegramBot.Services;
using OurFuss.Utils.TelegramBot.Services.Modules;

namespace OurFuss.Api.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        #region Facades
        
        services.AddScoped<IEventFacade, EventFacade>();
        services.AddScoped<IAdminFacade, AdminFacade>();

        #endregion

        #region Services

        //Api
        services.AddScoped<GwtTokenGenerator, GwtTokenGenerator>();

        //Core
        services.AddScoped<IEntityRepository, EntityRepository>();
        services.AddScoped<IEventService, EventService>();

        //Utils
        services.AddScoped<ITelegramBotService, TelegramBotService>();
        services.AddScoped<ITelegramBotCommandFactory, TelegramBotCommandFactory>();

        services.Scan(scan => scan.FromAssemblyOf<ITelegramBotCommand>().AddClasses().AsImplementedInterfaces().WithTransientLifetime());

        #endregion

        #region Repositories

        services.AddScoped<IEventRepository, EventRepository>();

        #endregion
    }
}