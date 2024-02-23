using ICS_project.BL.Models;
using ICS_project.DAL.Entities;

namespace ICS_project.BL.Mappers;

public class ActivityDetailModelMapper : IActivityDetailModelMapper
{
    private readonly IProjectDetailModelMapper _projectDetailModelMapper;
    private readonly IProjectListModelMapper _projectListModelMapper;
    private readonly IUserDetailModelMapper _userDetailModelMapper;
    private readonly ITagModelMapper _tagModelMapper;

    public ActivityDetailModelMapper(
        IProjectDetailModelMapper projectDetailModelMapper,
        IProjectListModelMapper projectListModelMapper,
        IUserDetailModelMapper userDetailModelMapper,
        ITagModelMapper tagModelMapper)
    {
        _projectDetailModelMapper = projectDetailModelMapper;
        _projectListModelMapper = projectListModelMapper;
        _userDetailModelMapper = userDetailModelMapper;
        _tagModelMapper = tagModelMapper;
    }
    public ActivityDetailModel MapToActivityDetailModel(ActivityEntity? entity)
        => entity is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Start = entity.Start,
                End = entity.End,
                Description = entity.Description,
                Project = _projectListModelMapper.MapToProjectListModelFromList(entity.Project),
                Tags = entity.Tags?.Select(t => _tagModelMapper.MapToTagModel(t.Tag)).ToList()
            };
    
    public IEnumerable<ActivityDetailModel> MapToActivityListModel(IEnumerable<ActivityEntity> entities)
        => entities.Select(MapToActivityDetailModel);

    public ActivityEntity MapToActivityEntityWithTagsProject(ActivityDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Start = model.Start,
            End = model.End,
            Description = model.Description,
            ProjectId = model.Project!.Id,
            Project = null,
            Tags = model.Tags?.Select(t => CreateTagActivityEntity(t, model.Id)).ToList() ?? new List<TagActivityEntity>(),
        };

    public ActivityEntity MapToActivityEntityWithTags(ActivityDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Start = model.Start,
            End = model.End,
            Description = model.Description,
            Tags = model.Tags?.Select(t => CreateTagActivityEntity(t, model.Id)).ToList() ?? new List<TagActivityEntity>(),
        };

    public ActivityEntity MapToActivityEntityWithProject(ActivityDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Start = model.Start,
            End = model.End,
            Description = model.Description,
            ProjectId = model.Project!.Id,
            Project = null,
        };

    public ActivityEntity MapToActivityEntityWithout(ActivityDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Start = model.Start,
            End = model.End,
            Description = model.Description,
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