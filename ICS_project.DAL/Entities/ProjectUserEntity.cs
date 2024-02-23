using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICS_project.DAL.Entities;

[Table("ProjectAmounts")]
public record ProjectUserEntity : IEntity
{
    [Key]
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public UserEntity? User { get; set; }
    public required Guid ProjectId { get; set; }
    [ForeignKey("ProjectId")]
    public ProjectEntity? Project { get; set; }
}