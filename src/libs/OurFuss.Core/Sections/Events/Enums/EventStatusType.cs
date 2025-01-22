using System.ComponentModel.DataAnnotations;

namespace OurFuss.Core.Sections.Events.Enums;

/// <summary>
/// Типа статуса события
/// </summary>
public enum EventStatusType
{
    [Display(Name = "Не определен")]
    Unknown = 0,

    [Display(Name = "В ожидании")]
    Pending = 1,

    [Display(Name = "Опубликовано")]
    Published = 2,
}
