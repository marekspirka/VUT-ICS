using ICS_project.DAL.Entities;

namespace ICS_project.DAL.Mappers;

public class ProjectUserEntityMapper : IEntityMapper<ProjectUserEntity>
{
    public void MapToExistingEntity(ProjectUserEntity existingEntity, ProjectUserEntity newEntity)
    {
        existingEntity.ProjectId = newEntity.ProjectId;
        existingEntity.UserId = newEntity.UserId;
    }
}