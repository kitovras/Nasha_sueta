using OurFuss.Api.Facades;
using OurFuss.Core.Modules.Event.Repositories;
using OurFuss.Core.Modules.Event.Services;

namespace OurFuss.Api.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        #region Facades

        services.AddScoped<IEventFacade, EventFacade>();

        #endregion

        #region Services

        services.AddScoped<IEventService, EventService>();

        #endregion

        #region Repositories

        services.AddScoped<IEventRepository, EventRepository>();

        #endregion
    }
}