using ICS_project.DAL.Entities;

namespace ICS_project.DAL.Mappers;

public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.Start = newEntity.Start;
        existingEntity.End = newEntity.End;
        existingEntity.Description = newEntity.Description;
        existingEntity.UserId = newEntity.UserId;
        existingEntity.ProjectId = newEntity.ProjectId;
    }
}