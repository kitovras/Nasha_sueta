using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFuss.Core.Db.Entities.Event;

/// <summary>
/// Событие
/// </summary>
[Table("Event", Schema = "ev")]
internal class EventEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    [Column("Name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Описание
    /// </summary>
    [Column("Description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Дата начала
    /// </summary>
    [Column("StartDate")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Навигационное свойство к тэгам события
    /// </summary>
    public virtual List<EventTagEntity>? EventTags { get; set; }
}