using AutoMapper;
using OurFuss.Core.Db;

namespace OurFuss.Core.Modules.Event.Domains;

internal static class EventMapper
{
    internal static IMapper MapEvent()
    {
        return new MapperConfiguration(mc =>
        {
            mc.CreateMap<EventEntity, Event>();
        }).CreateMapper();
    }
}