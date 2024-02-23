using ICS_project.BL.Mappers;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using ICS_project.DAL.Mappers;
using ICS_project.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ICS_project.BL.Facades;

public class TagFacade : ITagFacade
{
    protected readonly IUnitOfWorkFactory UnitOfWorkFactory;
    protected readonly ITagModelMapper ModelMapper;

    public TagFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        ITagModelMapper modelMapper)
    {
        UnitOfWorkFactory = unitOfWorkFactory;
        ModelMapper = modelMapper;
    }

    public async Task DeleteAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();
        try
        {
            uow.GetRepository<TagEntity, TagEntityMapper>().Delete(id);
            await uow.CommitAsync().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Entity deletion failed.", e);
        }
    }

    public virtual async Task<TagModel> SaveAsync(TagModel model, Guid userId)
    {
        TagModel result;
        var entity = ModelMapper.MapToTagEntity(model);
        var uow = UnitOfWorkFactory.Create();
        var repository = uow.GetRepository<TagEntity, TagEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            var updatedEntity = await repository.UpdateAsync(entity);
            updatedEntity.UserId = userId;
            result = ModelMapper.MapToTagModel(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            var insertedEntity = await repository.InsertAsync(entity);
            insertedEntity.UserId = userId;
            result = ModelMapper.MapToTagModel(insertedEntity);
        }

        await uow.CommitAsync();

        return result;
    }

    public virtual async Task<TagModel?> GetAsync(Guid id)
    {
        await using var uow = UnitOfWorkFactory.Create();

        var query = uow.GetRepository<TagEntity, TagEntityMapper>().Get();

        var entity = await query.SingleOrDefaultAsync(e => e.Id == id);

        return entity is null
            ? null
            : ModelMapper.MapToTagModel(entity);
    }

    public virtual async Task<IEnumerable<TagModel>> GetAsync()
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<TagEntity> entities = await uow
            .GetRepository<TagEntity, TagEntityMapper>()
            .Get()
            .ToListAsync();

        return ModelMapper.MapToTagListModel(entities);
    }
    
    public virtual async Task<IEnumerable<TagModel>> GetUsersAsync(Guid userId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<TagEntity> entities = uow
            .GetRepository<TagEntity, TagEntityMapper>()
            .Get();

        List<TagEntity> filteredEntities = await entities.Where(p => p.UserId == userId).ToListAsync();

        return ModelMapper.MapToTagListModel(filteredEntities);
    }
}