using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFuss.Core.Db.Entities.Event;

/// <summary>
/// Тэг события
/// </summary>
[Table("EventTag", Schema = "ev")]
internal class EventTagEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор события
    /// </summary>
    [Column("EventId")]
    public int EventId { get; set; }

    /// <summary>
    /// Идентификатор тэга
    /// </summary>
    [Column("TagId")]
    public int TagId { get; set; }

    /// <summary>
    /// Навигационное свойство к событию
    /// </summary>
    public virtual EventEntity? Event { get; set; }

    /// <summary>
    /// Навигационное свойство к тэгам
    /// </summary>
    public virtual TagEntity? Tag { get; set; }
}