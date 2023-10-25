using OurFuss.Api.Converters.Dto.Activities.Abstraction;

namespace OurFuss.Api.Converters.Dto.Activities.Response;

/// <summary>
/// Api-ответ событие по фильтру
/// </summary>
public class ApiEventByFilterResponse : ApiEventAbstract
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Тэги событий
    /// </summary>
    public List<ApiEventTagByFilterResponse> EventTags { get; set; } = new();
}
