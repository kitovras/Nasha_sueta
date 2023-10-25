using AutoMapper;
using OurFuss.Core.Db.Entities.Event;

namespace OurFuss.Core.Modules.Activities.Domains;

internal static class ActivityMapper
{
    internal static IMapper MapEventDomainToEventEntity()
    {
        return new MapperConfiguration(mc =>
        {
            mc.CreateMap<Event, EventEntity> ()
            .ForMember(fm => fm.Id, fm => fm.MapFrom(mf => mf.Id))
            .ForMember(fm => fm.Name, fm => fm.MapFrom(mf => mf.Name))
            .ForMember(fm => fm.Description, fm => fm.MapFrom(mf => mf.Description))
            .ForMember(fm => fm.EventTags, fm => fm.MapFrom(mf => new List<EventTagEntity>()))
            .ForMember(fm => fm.StartDate, fm => fm.MapFrom(mf => mf.StartDate));
        }).CreateMapper();
    }

    internal static IMapper MapEventEntityToEventDomain()
    {
        return new MapperConfiguration(mc =>
        {
            mc.CreateMap<EventEntity, Event>()
            .ForMember(fm => fm.Id, fm => fm.MapFrom(mf => mf.Id))
            .ForMember(fm => fm.Name, fm => fm.MapFrom(mf => mf.Name))
            .ForMember(fm => fm.Description, fm => fm.MapFrom(mf => mf.Description))
            .ForMember(fm => fm.StartDate, fm => fm.MapFrom(mf => mf.StartDate));
        }).CreateMapper();
    }

    internal static IMapper MapTagDomainToTagEntity()
    {
        return new MapperConfiguration(mc =>
        {
            mc.CreateMap<EventTag, TagEntity>()
            .ForMember(fm => fm.Id, fm => fm.MapFrom(mf => mf.Id))
            .ForMember(fm => fm.Name, fm => fm.MapFrom(mf => mf.Name));
        }).CreateMapper();
    }

    internal static IMapper MapTagEntityToTagDomain()
    {
        return new MapperConfiguration(mc =>
        {
            mc.CreateMap<TagEntity, EventTag>()
            .ForMember(fm => fm.Id, fm => fm.MapFrom(mf => mf.Id))
            .ForMember(fm => fm.Name, fm => fm.MapFrom(mf => mf.Name));
        }).CreateMapper();
    }
}