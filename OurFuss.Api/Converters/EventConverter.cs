using AutoMapper;
using OurFuss.Api.Converters.Dto.Activities.Response;
using OurFuss.Core.Modules.Activities.Domains;

namespace OurFuss.Api.Converters;

public static class EventConverter
{
    internal static IMapper MapApiEventTagToDomainEventTag()
    {
        return new MapperConfiguration(mc =>
        {
            mc.CreateMap<EventTag, ApiEventTagResponse>();
        }).CreateMapper();
    }

    internal static IMapper MapApiEventToEventDomain()
    {
        return new MapperConfiguration(mc =>
        {
            mc.CreateMap<ApiEventResponse, Event> ();
            mc.CreateMap<ApiEventTagResponse, EventTag> ();
        }).CreateMapper();
    }

    internal static IMapper MapEventDomainToApiEvent()
    {
        return new MapperConfiguration(mc =>
        {
            mc.CreateMap<Event, ApiEventByFilterResponse>();
            mc.CreateMap<EventTag, ApiEventTagByFilterResponse>();
        }).CreateMapper();
    }
}