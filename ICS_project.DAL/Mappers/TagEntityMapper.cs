using ICS_project.DAL.Entities;

namespace ICS_project.DAL.Mappers;

public class TagEntityMapper : IEntityMapper<TagEntity>
{
    public void MapToExistingEntity(TagEntity existingEntity, TagEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.UserId = newEntity.UserId;
    }
}