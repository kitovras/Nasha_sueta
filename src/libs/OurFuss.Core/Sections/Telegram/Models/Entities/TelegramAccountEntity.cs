using System.ComponentModel.DataAnnotations.Schema;
using OurFuss.Core.Sections.Entities.Enums;
using OurFuss.Core.Sections.Entities.Models.Base;
using OurFuss.Core.Sections.Events.Models.Entities;
using OurFuss.Core.Sections.Telegram.Enums;

namespace OurFuss.Core.Sections.Telegram.Models.Entities;

[Table("Account", Schema = "tg")]
public class TelegramAccountEntity : TableBaseEntity
{
    /// <inheritdoc/>
    protected override TableType TableType => TableType.TelegramAccount;

    [Column(nameof(TelegramId))]
    public long TelegramId { get; set; }

    /// <summary>
    /// Chat ID
    /// </summary>
    [Column(nameof(ChatId))]
    public long ChatId { get; set; }

    /// <summary>
    /// Full name
    /// </summary>
    [Column(nameof(FullName))]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Member status of the user
    /// </summary>
    [Column(nameof(UserMemberStatus))]
    public UserMemberStatus UserMemberStatus { get; set; }

    /// <summary>
    /// Telegram User Role Type
    /// </summary>
    [Column(nameof(TelegramRole))]
    public TelegramRoleType TelegramRole { get; set; }

    #region Navigations

    public ICollection<UserEventCustomEntity>? UserEventCustoms { get; set; }

    #endregion
}