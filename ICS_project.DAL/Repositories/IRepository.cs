using System;
using System.Linq;
using System.Threading.Tasks;
using ICS_project.DAL.Entities;

namespace ICS_project.DAL.Repositories;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    IQueryable<TEntity> Get();
    void Delete(Guid entityId);
    ValueTask<bool> ExistsAsync(TEntity entity);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
}
