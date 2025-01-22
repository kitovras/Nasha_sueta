using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OurFuss.Core.Sections.Entities.Enums;

namespace OurFuss.Core.Sections.Entities.Models.Base;

/// <summary>
/// Базовые поля таблицы
/// </summary>
public abstract class TableBaseEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column(nameof(Id))]
    public Guid Id { get; set; }

    /// <summary>
    /// Тип таблицы
    /// </summary>
    [NotMapped]
    protected virtual TableType TableType { get; set; } = TableType.Unknown;

    /// <summary>
    /// Дата и время добавления
    /// </summary>
    [Column(nameof(Created))]
    public DateTime Created { get; set; }

    /// <summary>
    /// Дата и время последнего изменения
    /// </summary>
    [Column(nameof(Modified))]
    public DateTime Modified { get; set; }
}