using OurFuss.Api.Converters.Dto.Activities.Response;

namespace OurFuss.Api.Converters.Dto.Activities.Request;

/// <summary>
/// Api-запрос на добавление тэгов
/// </summary>
public class ApiAddEventTagRequest
{
    /// <summary>
    /// Тэги событий
    /// </summary>
    public List<ApiEventTagResponse> EventTags { get; set; } = new();
}
