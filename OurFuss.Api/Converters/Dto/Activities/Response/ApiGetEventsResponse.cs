namespace OurFuss.Api.Converters.Dto.Activities.Response;

/// <summary>
/// Api-ответ на получение событий
/// </summary>
public class ApiGetEventsResponse
{
    /// <summary>
    /// События
    /// </summary>
    public List<ApiEventByFilterResponse> Events { get; set; } = new();
}