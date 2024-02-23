using Microsoft.EntityFrameworkCore;

namespace ICS_project.DAL.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<ICS_projectDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<ICS_projectDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}
