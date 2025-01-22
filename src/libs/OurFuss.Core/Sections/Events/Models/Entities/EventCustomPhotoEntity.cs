using System.ComponentModel.DataAnnotations.Schema;
using OurFuss.Core.Sections.Entities.Enums;
using OurFuss.Core.Sections.Entities.Models.Base;

namespace OurFuss.Core.Sections.Events.Models.Entities;

[Table("EventCustomPhoto", Schema = "evnt")]
public class EventCustomPhotoEntity : TableBaseEntity
{
    /// <inheritdoc/>
    [NotMapped]
    protected override TableType TableType => TableType.UserEventPhotoCustom;

    [ForeignKey(nameof(UserEventCustomId))]
    public Guid UserEventCustomId { get; set; }

    [Column(nameof(FileId))]
    public string FileId { get; set; }

    [Column(nameof(FileSize))]
    public long? FileSize { get; set; }

    [Column(nameof(Height))]
    public int Height { get; set; }

    [Column(nameof(Width))]
    public int Width { get; set; }

    [Column(nameof(FileUniqueId))]
    public string FileUniqueId { get; set; }

    #region Navigations

    public UserEventCustomEntity? UserEventCustom { get; set; } = null!;

    #endregion
}
