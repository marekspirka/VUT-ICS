using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICS_project.DAL.Entities;

[Table("Activities")]
public record ActivityEntity : IEntity
{
    [Key]
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set;  }
    public string? Description { get; set; }
    [ForeignKey("ProjectId")]
    public ProjectEntity? Project { get; set; }
    [ForeignKey("UserId")]
    public UserEntity? User { get; set; }
    public IList<TagActivityEntity> Tags { get; set; } = new List<TagActivityEntity>();

}