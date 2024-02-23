using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICS_project.DAL.Entities;

[Table("Users")]
public record UserEntity : IEntity
{
    [Key]
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? ImageURL { get; set; }
    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
    public ICollection<ProjectUserEntity> Projects { get; set; } = new List<ProjectUserEntity>();
}
