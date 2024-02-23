namespace ICS_project.BL.Models;

public record FilterModel : Model
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public ProjectDetailModel? Project { get; set; }
    public UserDetailModel User { get; set; }
    public ICollection<TagModel>? Tags { get; set; }

    public static FilterModel Empty => new()
    {
        Id = Guid.NewGuid(),
    };
}