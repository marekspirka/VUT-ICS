using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

namespace ICS_project.BL.Mappers;

public class ProjectDetailModelMapper : IProjectDetailModelMapper
{
    private readonly IUserDetailModelMapper _userDetailModelMapper;

    public ProjectDetailModelMapper(
        IUserDetailModelMapper userDetailModelMapper) =>
    _userDetailModelMapper = userDetailModelMapper;

    public ProjectDetailModel MapToProjectDetailModel(ProjectEntity? entity)
        => entity is null
            ? ProjectDetailModel.Empty
            : new ProjectDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Users = entity.Users.Select(t => _userDetailModelMapper.MapToUserDetailModel(t.User)).ToList(),
            };

    public ProjectEntity MapToProjectEntity(ProjectDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Users = model.Users?.Select(t => CreateProjectUserEntity(t, model.Id)).ToList() ?? new List<ProjectUserEntity>(),
        };

    private ProjectUserEntity CreateProjectUserEntity(UserDetailModel model, Guid projectId)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            UserId = model.Id,
            ProjectId = projectId
        };
    }
}