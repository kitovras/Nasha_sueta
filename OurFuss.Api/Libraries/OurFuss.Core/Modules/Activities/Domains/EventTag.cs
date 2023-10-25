namespace OurFuss.Core.Modules.Activities.Domains;

/// <summary>
/// Тэг события
/// </summary>
public class EventTag
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
