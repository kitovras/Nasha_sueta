namespace OurFuss.Api.Converters.Dto.Activities.Request;

/// <summary>
/// Api-запрос на получение событий
/// </summary>
public class ApiGetEventsRequest
{
    /// <summary>
    /// Текущая дата и время
    /// </summary>
    public DateTime CurrentDateTime { get; set; }

    /// <summary>
    /// Поисковая строка
    /// </summary>
    public string TextSearch { get; set; } = string.Empty;
}