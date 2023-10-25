namespace OurFuss.Core.Modules.Activities.Domains;

/// <summary>
/// Событие
/// </summary>
public class Event
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Дата начала
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Тэги события
    /// </summary>
    public List<EventTag> EventTags { get; set; } = new();
}