using ICS_project.BL.Mappers;
using ICS_project.BL.Models;
using ICS_project.DAL.Entities;
using ICS_project.DAL.Mappers;
using ICS_project.DAL.Repositories;
using ICS_project.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ICS_project.BL.Facades;

public class UserFacade : IUserFacade
{
    protected readonly IUnitOfWorkFactory UnitOfWorkFactory;
    protected readonly IUserDetailModelMapper ModelMapper;
    
    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserDetailModelMapper modelMapper)
    {
        UnitOfWorkFactory = unitOfWorkFactory;
        ModelMapper = modelMapper;
    }

    public async Task DeleteAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        uow.GetRepository<UserEntity, UserEntityMapper>().Delete(id);
        await uow.CommitAsync().ConfigureAwait(false);
    }
    
    public virtual async Task<UserDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<UserEntity> query = uow.GetRepository<UserEntity, UserEntityMapper>().Get();

        UserEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);

        return entity is null
            ? null
            : ModelMapper.MapToUserDetailModel(entity);
    }
    
    public virtual async Task<IEnumerable<UserDetailModel>> GetAsync()
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<UserEntity> entities = await uow
            .GetRepository<UserEntity, UserEntityMapper>()
            .Get()
            .ToListAsync();

        return ModelMapper.MapToUserDetailListModel(entities);
    }

    public virtual async Task<UserDetailModel> SaveAsync(UserDetailModel model)
    {
        UserDetailModel result;

        UserEntity entity = ModelMapper.MapToUserEntity(model);

        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<UserEntity> repository = uow.GetRepository<UserEntity, UserEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            UserEntity updatedEntity = await repository.UpdateAsync(entity);
            result = ModelMapper.MapToUserDetailModel(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            UserEntity insertedEntity = await repository.InsertAsync(entity);
            result = ModelMapper.MapToUserDetailModel(insertedEntity);
        }

        await uow.CommitAsync();

        return result;
    }
}

