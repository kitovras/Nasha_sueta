namespace OurFuss.Core.Sections.Events.Models.Domains;

public class UserEventCustomImage
{
    public Guid Id { get; set; }
    public string FileId { get; set; }
    public long? FileSize { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public string FileUniqueId { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
}
