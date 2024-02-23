using System.Diagnostics;
using System.Reflection.PortableExecutable;
using ICS_project.BL.Mappers;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using ICS_project.DAL.Mappers;
using ICS_project.DAL.Repositories;
using ICS_project.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.BL.Facades;

public class ProjectFacade : IProjectFacade
{
    protected readonly IUnitOfWorkFactory UnitOfWorkFactory;
    protected readonly IProjectDetailModelMapper DetailModelMapper;
    protected readonly IProjectListModelMapper ListModelMapper;
    
    public ProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectDetailModelMapper detailModelMapper,
        IProjectListModelMapper listModelMapper)
    {
        UnitOfWorkFactory = unitOfWorkFactory;
        DetailModelMapper = detailModelMapper;
        ListModelMapper = listModelMapper;
    }

    public async Task DeleteAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        uow.GetRepository<ProjectEntity, ProjectEntityMapper>().Delete(id);
        await uow.CommitAsync().ConfigureAwait(false);
    }
    
    public virtual async Task<ProjectDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ProjectEntity> query = uow.GetRepository<ProjectEntity, ProjectEntityMapper>().Get();
        List<UserEntity> users = await uow.GetRepository<UserEntity, UserEntityMapper>().Get().ToListAsync();

        await uow.GetRepository<ProjectUserEntity, ProjectUserEntityMapper>().Get().ToListAsync();
        ProjectEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);
        
        if(entity != null)
        {
            for (int i = 0; i < entity.Users.Count; i++)
            {
                entity.Users[i].User = users.Find(u => u.Id == entity.Users[i].UserId);
            }
        }

        return entity is null
            ? null
            : DetailModelMapper.MapToProjectDetailModel(entity);
    }
    
    public virtual async Task<IList<ProjectListModel>> GetAsync()
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ProjectEntity> entities = await uow
            .GetRepository<ProjectEntity, ProjectEntityMapper>()
            .Get()
            .ToListAsync();

        return ListModelMapper.MapToProjectListModel(entities).ToList();
    }

    public virtual async Task<IList<ProjectListModel>> GetUsersAsync(Guid userId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<ProjectEntity> entities = uow
            .GetRepository<ProjectEntity, ProjectEntityMapper>()
            .Get();

        List<ProjectEntity> filteredEntities = await entities.Where(p => p.Users.Any(u => u.UserId == userId)).ToListAsync();

        return ListModelMapper.MapToProjectListModel(filteredEntities).ToList();
    }

    public virtual async Task<bool> IsUserInProject(Guid userId, Guid projectId)
    {
        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ProjectUserEntity> repository = uow.GetRepository<ProjectUserEntity, ProjectUserEntityMapper>();
        List<ProjectUserEntity> projectUserEntities = await repository.Get().ToListAsync();
        return projectUserEntities.Find(p => (p.ProjectId == projectId && p.UserId == userId)) != null;
    }

    public virtual async Task<bool> ToggleProjectJoin(Guid userId, Guid projectId)
    {
        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ProjectUserEntity> repository = uow.GetRepository<ProjectUserEntity, ProjectUserEntityMapper>();
        List<ProjectUserEntity> projectUserEntities = await repository.Get().ToListAsync();

        bool isJoined;
        ProjectUserEntity? ExistingEntity = projectUserEntities.Find(p => (p.ProjectId == projectId && p.UserId == userId));

        if (ExistingEntity != null)
        {
            repository.Delete(ExistingEntity.Id);
            isJoined = false;
        }
        else
        {
            ProjectUserEntity entity = new ProjectUserEntity()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ProjectId = projectId
            };
            await repository.InsertAsync(entity);
            isJoined = true;
        }

        await uow.CommitAsync();
        return isJoined;
    }

    public virtual async Task<ProjectDetailModel> SaveAsync(ProjectDetailModel model)
    {
        ProjectDetailModel result;

        ProjectEntity entity = DetailModelMapper.MapToProjectEntity(model);

        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ProjectEntity> repository = uow.GetRepository<ProjectEntity, ProjectEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            ProjectEntity updatedEntity = await repository.UpdateAsync(entity);
            result = DetailModelMapper.MapToProjectDetailModel(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            ProjectEntity insertedEntity = await repository.InsertAsync(entity);
            result = DetailModelMapper.MapToProjectDetailModel(insertedEntity);
        }

        await uow.CommitAsync();

        return result;
    }
}