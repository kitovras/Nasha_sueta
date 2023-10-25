using OurFuss.Api.Converters.Dto.Activities.Abstraction;

namespace OurFuss.Api.Converters.Dto.Activities.Response;

/// <summary>
/// Api-ответ события 
/// </summary>
public class ApiEventResponse : ApiEventAbstract
{
    /// <summary>
    /// Тэги событий
    /// </summary>
    public List<ApiEventTagResponse> EventTags { get; set; } = new();
}
