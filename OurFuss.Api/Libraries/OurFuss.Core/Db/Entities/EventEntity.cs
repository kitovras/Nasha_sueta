using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFuss.Core.Db;

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
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    [Column("name")]
    public string Name { get; set; } = string.Empty;
}