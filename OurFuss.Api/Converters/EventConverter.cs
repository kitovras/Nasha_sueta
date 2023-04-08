using OurFuss.Core.Modules.Event.Domains;

namespace OurFuss.Api.Converters;

internal static class EventConverter
{
    internal static List<ApiEvent> EventConverted(List<Event> events)
    {
        return events.Select(s => new ApiEvent
        {
            Name = s.Name,
        }).ToList();
    }
}