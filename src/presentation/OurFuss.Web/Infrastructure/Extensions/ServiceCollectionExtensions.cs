using System.Diagnostics;
using OurFuss.Background.Hosted;
using OurFuss.Core.Sections.Entities;
using OurFuss.Core.Sections.Events.Services;
using OurFuss.Core.Sections.Telegram.Services;
using OurFuss.Core.Utils.Guid;
using OurFuss.Data.Postgre.Repositories;
using OurFuss.TelegramBot.EventGeneration;
using OurFuss.TelegramBot.EventGeneration.Services;

namespace OurFuss.Web.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ServiceCollection(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.ConfigureCoreServices(builder);
        services.ConfigureTelegramBotServices();
        services.ConfigureBackgroundServices();
        services.ConfigureFacades();
        services.ConfigureRepositories();
        services.ConfigureHelpers();
        services.ConfigureMappers();
    }

    #region Helpers

    private static void ConfigureTelegramBotServices(this IServiceCollection services)
    {
        //Utils => TelegramBot => EventGeneration
        services.AddScoped<ITelegramClientFactory, TelegramClientFactory>();
        services.AddScoped<ITelegramListenerService, TelegramListenerService>();
        services.AddScoped<ITelegramCommandFactory, TelegramCommandFactory>();

        services.Scan(scan => scan.FromAssemblyOf<ITelegramCommandModule>().AddClasses().AsImplementedInterfaces().WithTransientLifetime());
    }

    private static void ConfigureBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<TelegramBackgroundService>();
    }

    private static void ConfigureCoreServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        using var scope = builder.Services.BuildServiceProvider().CreateScope();

        //Api
        services.AddSingleton<Stopwatch>();

        //Core => Events
        services.AddScoped<IEventService, EventService>();

        //Core => Telegram
        services.AddScoped<ITelegramUserService, TelegramUserService>();

        //Core => Utils
        services.AddScoped<IGuidGenerator, GuidGenerator>();
    }

    private static void ConfigureFacades(this IServiceCollection services)
    {
        //services.AddScoped<IAdminFacade, AdminFacade>();
    }

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEntityRepository, EntityRepository>();
    }

    private static void ConfigureHelpers(this IServiceCollection services)
    {
        //services.AddScoped<IModelStateValidation, ModelStateValidation>();
        //services.AddScoped<ValidationRequestModels>();
        //services.AddScoped<IGwtTokenGenerator, GwtTokenGenerator>();
    }

    private static void ConfigureMappers(this IServiceCollection services)
    {
        //Mapper
        //var assemblies = new Assembly[] { typeof(MainProfile).Assembly };
        //services.AddAutoMapper((IServiceProvider f, IMapperConfigurationExpression cfg) =>
        //{
        //    cfg.AddProfile(typeof(MainProfile));
        //}, assemblies);
    }

    #endregion
}
