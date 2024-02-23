using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICS_project.DAL.Entities;

[Table("Tag")]
public record TagEntity : IEntity
{
    [Key]
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid UserId { get; set; }
    public ICollection<TagActivityEntity> Activities { get; set; } = new List<TagActivityEntity>();
}