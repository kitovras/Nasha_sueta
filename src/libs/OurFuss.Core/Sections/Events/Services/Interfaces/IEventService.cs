using OurFuss.Core.Sections.Events.Enums;
using OurFuss.Core.Sections.Events.Models.Domains;

namespace OurFuss.Core.Sections.Events.Services;

public interface IEventService
{
    Task AddUserEventAsync(UserEventCustom addUserEventModelInput);
    Task<int> GetEventCountAsync(EventStatusType eventStatus);
    Task<UserEventCustom?> GetUserEventAsync(Guid customEventId);
    Task<UserEventCustom?> GetUserEventCustomWithTelegramAccountAsync(Guid customEventId);
    Task<UserEventCustom?> GetUserEventFullAsync(Guid customEventId);
    Task<List<UserEventCustom>> GetUserEventsAsync(int skip, int take, EventStatusType eventStatus);
    Task SaveUserEventAsync(UserEventCustom userEventCustom);
    Task<bool> UserEventCustomRemoveByIdAsync(Guid customEventId);
}
