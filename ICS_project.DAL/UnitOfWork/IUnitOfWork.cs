using System;
using System.Threading.Tasks;
using ICS_project.DAL.Entities;
using ICS_project.DAL.Mappers;
using ICS_project.DAL.Repositories;

namespace ICS_project.DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity, TEntityMapper>()
        where TEntity : class, IEntity
        where TEntityMapper : IEntityMapper<TEntity>, new();

    Task CommitAsync();
}
