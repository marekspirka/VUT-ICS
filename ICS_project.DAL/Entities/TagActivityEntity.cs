using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICS_project.DAL.Entities;

[Table("TagAmounts")]
public record TagActivityEntity : IEntity
{
    [Key]
    public required Guid Id { get; set; }
    public required Guid ActivityId { get; set; }
    [ForeignKey("ActivityId")]
    public ActivityEntity? Activity { get; set; }
    public required Guid TagId { get; set; }
    [ForeignKey("TagId")]
    public TagEntity? Tag { get; set; }
}