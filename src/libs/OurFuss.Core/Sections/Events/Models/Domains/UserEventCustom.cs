using OurFuss.Core.Sections.Events.Enums;
using OurFuss.Core.Sections.Telegram.Models.Domains;

namespace OurFuss.Core.Sections.Events.Models.Domains;

public class UserEventCustom
{
    public Guid Id { get; set; }

    public Guid TelegramId { get; set; }

    public string MessageText { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }

    public UserEventCustomImage? UserEventCustomImage { get; set; } = null;

    public TelegramAccount? TelegramAccount { get; set; } = null;

    public DateTime? DeferredPublication { get; set; }

    public EventStatusType EventStatus { get; set; }
}
