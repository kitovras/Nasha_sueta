using System.ComponentModel.DataAnnotations.Schema;
using OurFuss.Core.Sections.Entities.Enums;
using OurFuss.Core.Sections.Entities.Models.Base;
using OurFuss.Core.Sections.Events.Enums;
using OurFuss.Core.Sections.Telegram.Models.Entities;

namespace OurFuss.Core.Sections.Events.Models.Entities;

[Table("UserEventCustom", Schema = "evnt")]
public class UserEventCustomEntity : TableBaseEntity
{
    /// <inheritdoc/>
    [NotMapped]
    protected override TableType TableType => TableType.UserEventCustom;

    [ForeignKey(nameof(TelegramAccountId))]
    public Guid TelegramAccountId { get; set; }

    [Column(nameof(Text))]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Дата и время отложенной публикации
    /// </summary>
    [Column(nameof(DeferredPublication))]
    public DateTime? DeferredPublication { get; set; }

    [Column(nameof(EventStatus))]
    public EventStatusType EventStatus { get; set; }

    #region Navigations

    public TelegramAccountEntity? TelegramAccount { get; set; }
    public ICollection<EventCustomPhotoEntity>? Photos { get; set; }

    #endregion
}
