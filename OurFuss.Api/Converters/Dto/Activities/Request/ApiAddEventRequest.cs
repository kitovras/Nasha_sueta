using OurFuss.Api.Converters.Dto.Activities.Response;

namespace OurFuss.Api.Converters.Dto.Activities.Request;

/// <summary>
/// Api-запрос на добавление события
/// </summary>
public class ApiAddEventRequest
{
    /// <summary>
    /// События
    /// </summary>
    public List<ApiEventResponse> Events { get; set; } = new();
}