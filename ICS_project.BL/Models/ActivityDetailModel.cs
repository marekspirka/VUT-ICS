namespace ICS_project.BL.Models;

public record ActivityDetailModel : Model
{
    public required string Name { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public string? Description { get; set; }
    public ProjectListModel? Project { get; set; }
    public ICollection<TagModel>? Tags { get; set; }

    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Start = DateTime.Now,
        End = DateTime.Now,
        Tags = new List<TagModel>()
    };
}