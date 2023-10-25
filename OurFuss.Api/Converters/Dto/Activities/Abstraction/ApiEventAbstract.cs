namespace OurFuss.Api.Converters.Dto.Activities.Abstraction;

/// <summary>
/// Api-модель события
/// </summary>
public class ApiEventAbstract
{
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
}
