using System;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

namespace ICS_project.BL.Mappers;

public class FilterModelMapper : IFilterModelMapper
{
    private readonly IProjectDetailModelMapper _projectDetailModelMapper;
    private readonly ITagModelMapper _tagModelMapper;
    private readonly IUserDetailModelMapper _userDetailModelMapper;

    public FilterModelMapper(
        IProjectDetailModelMapper projectDetailModelMapper,
        ITagModelMapper tagModelMapper,
        IUserDetailModelMapper userDetailModelMapper)
    {
        _projectDetailModelMapper = projectDetailModelMapper;
        _tagModelMapper = tagModelMapper;
        _userDetailModelMapper = userDetailModelMapper;
    }
    public IEnumerable<FilterModel> MapToFilterModel(IEnumerable<ActivityEntity> entities)
        => entities.Select(MapToFilterModel);

    public FilterModel MapToFilterModel(ActivityEntity? entity)
        => entity is null
            ? FilterModel.Empty
            : new FilterModel
            {
                Id = entity.Id,
                Start = entity.Start,
                End = entity.End,
                Project = _projectDetailModelMapper.MapToProjectDetailModel(entity.Project),
                Tags = entity.Tags.Select(t => _tagModelMapper.MapToTagModel(t.Tag)).ToList()
            };

    public ActivityEntity MapToActivityEntity(FilterModel model)
        => new()
        {
            Id = model.Id,
            Name = String.Empty,
            Start = model.Start,
            End = model.End,
            ProjectId = model.Project.Id,
            Project = _projectDetailModelMapper.MapToProjectEntity(model.Project),
            UserId = model.User.Id,
            User = _userDetailModelMapper.MapToUserEntity(model.User),
            Tags = model.Tags?.Select(t => CreateTagActivityEntity(t, model.Id)).ToList() ?? new List<TagActivityEntity>(),
        };

    private TagActivityEntity CreateTagActivityEntity(TagModel model, Guid activityId)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            ActivityId = activityId,
            TagId = model.Id
        };
    }
}