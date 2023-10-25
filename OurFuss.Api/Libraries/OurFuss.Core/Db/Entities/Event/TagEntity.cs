using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OurFuss.Core.Db.Entities.Event;

/// <summary>
/// Тэг
/// </summary>
[Table("Tag", Schema = "ev")]
internal class TagEntity
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
}
