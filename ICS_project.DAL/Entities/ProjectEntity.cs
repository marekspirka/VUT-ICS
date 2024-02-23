using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICS_project.DAL.Entities;

[Table("Projects")]
public record ProjectEntity : IEntity
{
    [Key]
    public required Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();

    public IList<ProjectUserEntity> Users { get; set; } = new List<ProjectUserEntity>();

}