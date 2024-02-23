using ICS_project.BL.Mappers;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using ICS_project.DAL.Mappers;
using ICS_project.DAL.Repositories;
using ICS_project.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.BL.Facades;

public class FilterFacade : IFilterFacade
{
    public FilterFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
         IActivityDetailModelMapper modelMapper)
    {
        UnitOfWorkFactory = unitOfWorkFactory;
        ModelMapper = modelMapper;
    }

    protected readonly IUnitOfWorkFactory UnitOfWorkFactory;
    protected readonly IActivityDetailModelMapper ModelMapper;

    public async Task<IEnumerable<ActivityDetailModel>> GetByEverythingAsync(Guid userId, DateTime? startDate, DateTime? endDate, DateTime startTime, DateTime endTime, Guid? tagId, Guid? projectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        var activityEntities = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        var filteredActivity = activityEntities.Where(p => p.UserId == userId);

        if (startDate.HasValue)
        {
            var startDay = startDate.Value.Date;
            filteredActivity = filteredActivity.Where(activity => activity.Start >= startDay);
        }

        if (endDate.HasValue)
        {
            var endDay = endDate.Value.Date;
            filteredActivity = filteredActivity.Where(activity => activity.End <= endDay);
        }

        if (tagId.HasValue)
        {
            filteredActivity = filteredActivity.Where(activity => activity.Tags.Any(tag => tag.TagId == tagId));
        }

        if (projectId.HasValue)
        {
            filteredActivity = filteredActivity.Where(activity => activity.Project.Id == projectId);
        }

        List<ActivityEntity> filteredActivityList = await filteredActivity.ToListAsync();

        List<ActivityEntity> finalActivityList = new List<ActivityEntity>();
        for (int i = 0; i < filteredActivityList.Count; i++)
        {
            if (filteredActivityList[i].Start.TimeOfDay >= startTime.TimeOfDay &&
                filteredActivityList[i].End.TimeOfDay <= endTime.TimeOfDay)
            {
                finalActivityList.Add(filteredActivityList[i]);
            }
        }
        await uow.GetRepository<TagActivityEntity, TagActivityEntityMapper>().Get().ToListAsync();

        List<ProjectEntity> projects = await uow.GetRepository<ProjectEntity, ProjectEntityMapper>().Get().ToListAsync();
        List<TagEntity> tags = await uow.GetRepository<TagEntity, TagEntityMapper>().Get().ToListAsync();

        for (int i = 0; i < finalActivityList.Count; i++)
        {
            finalActivityList[i].Project = projects.Find(p => p.Id == finalActivityList[i].ProjectId);
            for (int j = 0; j < filteredActivityList[i].Tags.Count; j++)
            {
                finalActivityList[i].Tags[j].Tag = tags.Find(t => t.Id == finalActivityList[i].Tags[j].TagId);
            }
        }

        return ModelMapper.MapToActivityListModel(finalActivityList);
    }
}