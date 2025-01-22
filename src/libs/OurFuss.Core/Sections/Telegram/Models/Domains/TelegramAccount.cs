using OurFuss.Core.Sections.Telegram.Enums;

namespace OurFuss.Core.Sections.Telegram.Models.Domains;

public class TelegramAccount
{
    public Guid Id { get; set; }

    public long TelegramId { get; set; }

    public long ChatId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public UserMemberStatus UserMemberStatus { get; set; }

    public TelegramRoleType TelegramRole { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
}
