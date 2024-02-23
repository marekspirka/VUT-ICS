using ICS_project.BL.Mappers;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using ICS_project.DAL.Mappers;
using ICS_project.DAL.Repositories;
using ICS_project.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.BL.Facades;

public class ActivityFacade : IActivityFacade
{
    protected readonly IUnitOfWorkFactory UnitOfWorkFactory;
    protected readonly IActivityDetailModelMapper ModelMapper;

    public ActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityDetailModelMapper modelMapper)
    {
        UnitOfWorkFactory = unitOfWorkFactory;
        ModelMapper = modelMapper;
    }

    public async Task DeleteAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Delete(id);
        await uow.CommitAsync().ConfigureAwait(false);
    }

    public virtual async Task<ActivityDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();
        
        await uow.GetRepository<TagActivityEntity, TagActivityEntityMapper>().Get().ToListAsync();

        ActivityEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);

        if (entity != null)
        {
            List<ProjectEntity> projects =
                await uow.GetRepository<ProjectEntity, ProjectEntityMapper>().Get().ToListAsync();
            List<TagEntity> tags = await uow.GetRepository<TagEntity, TagEntityMapper>().Get().ToListAsync();

            entity.Project = projects.Find(p => p.Id == entity.ProjectId);
            for (int i = 0; i < entity.Tags.Count; i++)
            {
                entity.Tags[i].Tag = tags.Find(t => t.Id == entity.Tags[i].TagId);
            }
        }

        return entity is null
            ? null
            : ModelMapper.MapToActivityDetailModel(entity);
    }
    
    public virtual async Task<IEnumerable<ActivityDetailModel>> GetUsersAsync(Guid userId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<ActivityEntity> entities = uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get();

        List<ActivityEntity> filteredEntities = await entities.Where(p => p.UserId == userId).ToListAsync();
        await uow.GetRepository<TagActivityEntity, TagActivityEntityMapper>().Get().ToListAsync();

        List<ProjectEntity> projects = await uow.GetRepository<ProjectEntity, ProjectEntityMapper>().Get().ToListAsync();
        List<TagEntity> tags = await uow.GetRepository<TagEntity, TagEntityMapper>().Get().ToListAsync();

        for (int i = 0; i < filteredEntities.Count; i++)
        {
            filteredEntities[i].Project = projects.Find(p => p.Id == filteredEntities[i].ProjectId);
            for (int j = 0; j < filteredEntities[i].Tags.Count; j++)
            {
                filteredEntities[i].Tags[j].Tag = tags.Find(t => t.Id == filteredEntities[i].Tags[j].TagId);
            }
        }

        return ModelMapper.MapToActivityListModel(filteredEntities);
    }

    public virtual async Task<ActivityDetailModel> SaveAsync(ActivityDetailModel model, Guid userId)
    {
        ActivityDetailModel result;
        ActivityEntity entity;
        if (model.Tags.Count != 0 && model.Project != null)
        {
            entity = ModelMapper.MapToActivityEntityWithTagsProject(model);
        }
        else if (model.Tags.Count != 0)
        {
            entity = ModelMapper.MapToActivityEntityWithTags(model);
        }
        else if (model.Project != null)
        {
            entity = ModelMapper.MapToActivityEntityWithProject(model);
        }
        else
        {
            entity = ModelMapper.MapToActivityEntityWithout(model);
        }

        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        if (model.Start >= model.End)
        {
            throw new Exception("The selected start is after selected end.");
        }

        bool isTimeSlotFull = await IsTimeSlotFullAsync(userId, model.Id, model.Start, model.End);

        if (isTimeSlotFull)
        {
            throw new Exception("The selected time slot is already occupied by another activity.");
        }

        if (await repository.ExistsAsync(entity))
        {
            var updatedEntity = await repository.UpdateAsync(entity);
            updatedEntity.UserId = userId;
            result = ModelMapper.MapToActivityDetailModel(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            var insertedEntity = await repository.InsertAsync(entity);
            insertedEntity.UserId = userId;
            result = ModelMapper.MapToActivityDetailModel(insertedEntity);
        }

        await uow.CommitAsync();

        return result;
    }

    public async Task<bool> IsTimeSlotFullAsync(Guid userId, Guid id, DateTime start, DateTime end)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> filteredActivity = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        filteredActivity = filteredActivity.Where(activity => activity.Id != id);
        filteredActivity = filteredActivity.Where(activity => activity.UserId == userId);
        filteredActivity = filteredActivity.Where(activity => (start >= activity.Start && start < activity.End) ||
                                                              (end > activity.Start && end <= activity.End) ||
                                                               end == activity.Start || start == activity.End ||
                                                              (start <= activity.Start && end >= activity.End));

        var isTimeSlotFull = filteredActivity.ToList().Any();

        return isTimeSlotFull;
    }
}